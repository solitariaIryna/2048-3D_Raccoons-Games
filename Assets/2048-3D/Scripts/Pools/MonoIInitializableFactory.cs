using System;
using Object = UnityEngine.Object;

namespace G2048_3D.Pool
{
    public class MonoIInitializableFactory<T> : IFactory<T> where T : Object, IInitializable
    {
        private readonly T _prefab;
        private readonly Action<T> _constructAction;

        public MonoIInitializableFactory(T prefab, Action<T> constructAction)
        {
            _prefab = prefab;
            _constructAction = constructAction;
        }

        public T CreateNewItem()
        {
            T newObject = Object.Instantiate(_prefab);

            _constructAction?.Invoke(newObject);
            newObject.Initialize();

            return newObject;
        }
    }
}