using G2048_3D.Gameplay.Core;
using System;

namespace G2048_3D.UI
{
    public abstract class BaseDisplay : MonoInitializeable
    {
        public Action<BaseDisplay> Disabled;
        protected override void OnInitialize() => 
            Subscribe();
        public virtual void Deactivate()
        {
            UnSubscribe();
            Disabled?.Invoke(this);
            Destroy(gameObject);
        }

        protected virtual void Subscribe() { }
        protected virtual void UnSubscribe() { }

    }
}
