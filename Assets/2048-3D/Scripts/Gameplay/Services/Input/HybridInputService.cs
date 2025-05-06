using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace G2048_3D.Gameplay.Services.Input
{
    public class HybridInputService : IInputService
    {
        private InputActions _inputActions;
        public Vector2 Axis => _inputActions.Player.Move.ReadValue<Vector2>();

        public event Action MouseUp;

        private bool _isHolded;
        public HybridInputService()
        {
            _inputActions = new();
           // _inputActions.Enable();

            //_inputActions.Player.Move.started += OnStarted;
            //_inputActions.Player.Move.canceled += OnClickUp;
        }
        private void OnStarted(InputAction.CallbackContext context)
        {
            _isHolded = true;
            Debug.Log("Started");
        }

        private void OnClickUp(InputAction.CallbackContext context)
        {
            if (_isHolded)
            {
                Debug.Log("Holded");
                MouseUp?.Invoke();
                _isHolded = false;
            }
               

          
        }
    }
}
