using G2048_3D.Gameplay.Services;
using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Player
{
    public class GhostTarget : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _limitX;
        [SerializeField] private float _zPosition;

        private Vector3 _startPosition;
        private IInputService _inputService;

        private bool _isEnable;
        public void Construct(IInputService inputService) => 
            _inputService = inputService;
        public void Initialize() =>
            _startPosition = transform.position;

        public void Enable(bool value) => 
            _isEnable = value;

        public void ResetPosition() => 
            transform.position = _startPosition;
      
        private void MoveGhost(float xInput)
        {
            if (Mathf.Abs(xInput) > 0.01f)
            {
                float addValue = xInput * _speed * Time.deltaTime;
                Vector3 newPosition = transform.position + Vector3.right * addValue;
                newPosition.z = _zPosition;
                newPosition.x = Mathf.Clamp(newPosition.x, -_limitX, _limitX);
                transform.position = newPosition;
            }
        }
        private void Update()
        {
            if (!_isEnable)
                return;

            float inputX = _inputService.Axis.x;
            MoveGhost(inputX);
        }
    }
}
