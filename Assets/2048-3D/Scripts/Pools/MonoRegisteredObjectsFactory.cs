using G2048_3D.Services.AssetProvider;
using UnityEngine;
using Zenject;

namespace G2048_3D.Pool
{
    public class MonoRegisteredObjectsFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;

        public MonoRegisteredObjectsFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }
        public T CreateObject<T>(T prefab) where T : MonoBehaviour
        {
            return _assetProvider.Instantiate<T>(prefab, _diContainer);
        }
    }
}