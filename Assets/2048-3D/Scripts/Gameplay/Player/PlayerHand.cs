using G2048_3D.Gameplay.Services;
using G2048_3D.Gameplay.Services.Input;
using UnityEngine;
using Zenject;

namespace G2048_3D.Gameplay.Player
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private float _pushForce = 100;
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
          //  _inputService.MouseUp += PushTarget;
            _cubeService.Spawner.PushedCubeSpawned += SetPushableTarget;
            _moveTarget.MouseUp += PushTarget;
        }
        private void Update()
        {
            if (_pushableTarget == null)
                return;

            _pushableTarget.Position = _moveTarget.transform.position;
        }
        private void OnDisable()
        {
          //  _inputService.MouseUp -= PushTarget;
            _cubeService.Spawner.PushedCubeSpawned -= SetPushableTarget;
            _moveTarget.MouseUp -= PushTarget;
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

        public void SetPushableTarget(IPushable pushableTarget)
        {
            _pushableTarget = pushableTarget;
            _moveTarget.Enable(true);
        }

    }
}
