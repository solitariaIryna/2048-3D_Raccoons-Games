using UnityEngine;
using Zenject;

namespace G2048_3D.Gameplay.Core
{
    public abstract class MonoInitializeable : MonoBehaviour, IInitializable
    {
        [SerializeField] private bool _autoInitializeOnStart = true;
        [Space]

        private bool _initialized;

        private void Start()
        {
            if (_autoInitializeOnStart)
                Initialize();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            OnInitialize();
        }

        protected abstract void OnInitialize();
    }

}
