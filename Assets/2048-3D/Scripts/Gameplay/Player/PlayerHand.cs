using G2048_3D.Gameplay.Core;
using G2048_3D.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace G2048_3D.Gameplay.Player
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private Vector3 _pushForce = new Vector3(0, 0, 70);
        [SerializeField] private GhostTarget _moveTarget;

        private IInputService _inputService;
        private CubeService _cubeService;

        private IPushable _pushableTarget;

        [Inject]
        private void Construct(IInputService inputService, CubeService cubeService)
        {
            _inputService = inputService;
            _cubeService = cubeService;
            _moveTarget.Construct(_inputService);
           
        }
        private void Awake()
        {
            _moveTarget.Initialize();
            _cubeService.Spawner.PushedCubeSpawned += SetPushableTarget;
            _inputService.MouseUp += PushTarget;
        }
        private void Update()
        {
            if (_pushableTarget == null)
                return;

            _pushableTarget.Position = _moveTarget.transform.position;
        }
        private void OnDisable()
        {
            _cubeService.Spawner.PushedCubeSpawned -= SetPushableTarget;
            _inputService.MouseUp -= PushTarget;
        }
        private void PushTarget()
        {
            if (_pushableTarget == null)
                return;

            _pushableTarget.Push(_pushForce);
            _pushableTarget = null;
            _moveTarget.ResetPosition();
            _moveTarget.Enable(false);
        }

        public void SetPushableTarget(IPushableProvider pushableTarget)
        {
            _pushableTarget = pushableTarget.Pushable;
            _moveTarget.Enable(true);
        }

    }
}
