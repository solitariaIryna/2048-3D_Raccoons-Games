using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class MobileInputService : MonoBehaviour, IInputService
    {
        public Vector2 Axis { get; private set; }

        public event Action MouseUp;
        private bool _isPressed;

        private void Update()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                _isPressed = true;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                Axis = new Vector2(touch.deltaPosition.x / Screen.dpi * 100f, touch.deltaPosition.y / Screen.dpi * 100f);  

            if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && _isPressed)
            {
                MouseUp?.Invoke();
                _isPressed = false;
            }
        }
    }
}
