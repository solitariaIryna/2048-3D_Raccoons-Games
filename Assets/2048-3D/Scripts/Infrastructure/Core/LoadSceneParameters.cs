using System;

namespace G2048_3D.Core
{
    public struct LoadSceneParameters
    {
        public readonly string Name;
        public readonly bool CanReload;
        public readonly Action OnLoaded;

        public LoadSceneParameters(string name, bool canReload, Action onLoaded = null)
        {
            Name = name;
            CanReload = canReload;
            OnLoaded = onLoaded;
        }
    }
}
    
