using UnityEngine;

namespace G2048_3D.Pool
{
    public class ObjectFactory<T> : IPoolFactory<T> where T : Object
    {
        private readonly T _prefab;

        public ObjectFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T CreateNewItem()
        {
            return UnityEngine.Object.Instantiate(_prefab);
        }
    }
}