using System;
using UnityEngine;

namespace G2048_3D.Pool
{
    public class ChainPool<T> : IPool<T> where T : Component
    {
        private T _prefab;
        private int _itemsCount;

        private T[] _createdItems;
        private int _currentIndex;

        private Transform _container;

        public T Prefab => _prefab;

        public ChainPool(T prefab, int count)
        {
            _prefab = prefab;
            _itemsCount = count;
        }

        public T GetItem()
        {
            int itemIndex = _currentIndex;

            IncreaseIndex();

            return _createdItems[itemIndex];
        }

        public void Initialize(IPoolFactory<T> factory, Transform container, Action<T> actionOnCreate)
        {
            _container = container;
            CreateItems(factory, actionOnCreate);
        }

        private void CreateItems(IPoolFactory<T> factory, Action<T> actionOnCreate)
        {
            _createdItems = new T[_itemsCount];

            for (int i = 0; i < _createdItems.Length; i++)
            {
                T newItem = factory.CreateNewItem();
                newItem.transform.parent = _container;
                actionOnCreate?.Invoke(newItem);

                _createdItems[i] = newItem;
            }
        }

        private void IncreaseIndex()
        {
            _currentIndex += 1;

            if (_currentIndex >= _createdItems.Length)
                _currentIndex = 0;
        }
    }
}