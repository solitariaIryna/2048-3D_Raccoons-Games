using G2048_3D.Constants;
using G2048_3D.Gameplay.Services.Input;
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
        private bool _isPressed;

        public Action MouseUp;
        public void Construct(IInputService inputService) => 
            _inputService = inputService;
        public void Initialize() =>
            _startPosition = transform.position;

        public void Enable(bool value) => 
            _isEnable = value;

        public void ResetPosition() => 
            transform.position = _startPosition;
        private void HandleTouchInput()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                _isPressed = true;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                float xInput = touch.deltaPosition.x / Screen.dpi * 100f;
                MoveGhost(xInput);
            }

            if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && _isPressed)
            {
                MouseUp?.Invoke();
                _isPressed = false;
            }
               
        }
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                _isPressed = true;

            if (Input.GetMouseButton(0))
            {
                float xInput = Input.GetAxis("Mouse X");
                MoveGhost(xInput);
            }

            if (Input.GetMouseButtonUp(0) && _isPressed)
            {
                MouseUp?.Invoke();
                _isPressed = false;
            } 
        }
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

#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif
        }
    }
}
