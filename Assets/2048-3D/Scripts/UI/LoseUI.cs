using System;
using UnityEngine;
using UnityEngine.UI;

namespace G2048_3D.UI
{
    public class LoseUI : BaseDisplay
    {
        [SerializeField] private Button _button;

        private Action _onRestartButtonPressed;
        public void Construct(Action onRestartButtonPressed) => 
            _onRestartButtonPressed = onRestartButtonPressed;
        protected override void Subscribe() => 
            _button.onClick.AddListener(RestartButtonPressed);
        protected override void UnSubscribe() => 
            _button.onClick.RemoveAllListeners();
        private void RestartButtonPressed()
        {
            UnSubscribe();
            _onRestartButtonPressed?.Invoke();
            Deactivate();
        }
    }
}
