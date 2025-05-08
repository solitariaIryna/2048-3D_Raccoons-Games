using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class StandaloneInputService : MonoBehaviour, IInputService
    {
        public Vector2 Axis { get; private set; }

        public event Action MouseUp;

        private bool _isPressed;

        private void Update()
        {
            Axis = Vector2.zero;

            if (Input.GetMouseButtonDown(0))
                _isPressed = true;

            if (Input.GetMouseButton(0))
            {
                Axis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }

            if (Input.GetMouseButtonUp(0) && _isPressed)
            {
                MouseUp?.Invoke();
                _isPressed = false;
            }
        }
    }
}
