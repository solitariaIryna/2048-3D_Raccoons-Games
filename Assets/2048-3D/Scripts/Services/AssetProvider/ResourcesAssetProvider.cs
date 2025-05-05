using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace G2048_3D.Services.AssetProvider
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        public TAsset[] LoadAll<TAsset>(string path) where TAsset : Object =>
            Resources.LoadAll<TAsset>(path);

        public async UniTask<TAsset> LoadAsync<TAsset>(string path) where TAsset : Object
        {
            ResourceRequest request = Resources.LoadAsync<TAsset>(path);
            await request;
            return request.asset as TAsset;
        }

        public async UniTask<TAsset[]> LoadAllAsync<TAsset>(string path) where TAsset : Object
        {
            ResourceRequest request = Resources.LoadAsync<TAsset>(path);
            await request;
            return request.asset as TAsset[];
        }

        public T Instantiate<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public T Instantiate<T>(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            T prefab = Load<T>(path);
            return prefab != null ? Object.Instantiate(prefab, position, rotation, parent) : null;
        }

        public T Instantiate<T>(T prefab, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            return container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }

        public T Instantiate<T>(string path, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            T prefab = Load<T>(path);
            return prefab != null ? container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent) : null;
        }

        public async UniTask<T> InstantiateAsync<T>(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            T prefab = await LoadAsync<T>(path);
            return prefab != null ? Object.Instantiate(prefab, position, rotation, parent) : null;
        }

        public async UniTask<T> InstantiateAsync<T>(string path, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            T prefab = await LoadAsync<T>(path);
            return prefab != null ? container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent) : null;
        }
    }
}