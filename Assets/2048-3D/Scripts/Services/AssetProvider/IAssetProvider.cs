using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace G2048_3D.Services.AssetProvider
{
    public interface IAssetProvider
    {
        TAsset Load<TAsset>(string path) where TAsset : Object;
        TAsset[] LoadAll<TAsset>(string path) where TAsset : Object;
        UniTask<TAsset> LoadAsync<TAsset>(string path) where TAsset : Object;
        UniTask<TAsset[]> LoadAllAsync<TAsset>(string path) where TAsset : Object;

        T Instantiate<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;
        T Instantiate<T>(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;
        T Instantiate<T>(T prefab, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;
        T Instantiate<T>(string path, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;

        UniTask<T> InstantiateAsync<T>(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;
        UniTask<T> InstantiateAsync<T>(string path, DiContainer container, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object;
    }
}