using G2048_3D.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace G2048_3D.Pool
{
    public class MonoStackPool<T> where T : MonoBehaviour, IReleaseable<T>, IResetable
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialCount = 16;
        [Space]
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _disableEnableItems = true;

        private IFactory<T> _factory;
        private List<T> _freeItems;

        private Action<T> _onReleasedAction;

        public T Prefab => _prefab;

        public MonoStackPool(T prefab, int initialCount, Transform parent, bool disableEnableItems)
        {
            _prefab = prefab;
            _initialCount = initialCount;
            _parent = parent;
            _disableEnableItems = disableEnableItems;
        }

        public MonoStackPool() : this(null, GameConstants.DEFAULT_POOL_ELEMENTS_COUNT, null, true) { }

        public static MonoStackPool<T> CreateInitializableFast<T>(T prefab, int initialCount, Transform parent, bool disableEnableItems, Action<T> constructAction) 
            where T : MonoBehaviour, IInitializable, IResetable, IReleaseable<T>
        {
            MonoIInitializableFactory<T> factory = new MonoIInitializableFactory<T>(prefab, constructAction);
            MonoStackPool<T> pool = new MonoStackPool<T>(prefab, initialCount, parent, disableEnableItems);
            pool.Initialize(factory);

            return pool;
        }

        public void Initialize(IFactory<T> factory)
        {
            _onReleasedAction = PushItem;
            _factory = factory;

            CreateFirstItems();
        }

        public T GetItem()
        {
            T item;

            if (_freeItems.Count > 0)
            {
                item = _freeItems[_freeItems.Count - 1];
                _freeItems.RemoveAt(_freeItems.Count - 1);
                item.ResetData();
            }
            else
            {
                item = CreateNewItem();
            }

            if (_disableEnableItems)
                item.gameObject.SetActive(true);

            return item;
        }

        private void PushItem(T item)
        {
#if UNITY_EDITOR
            if (_freeItems.Contains(item))
                Debug.LogError($"Duplicated Item Detected! {item.name}", item);
#endif

            if (_disableEnableItems)
                item.gameObject.SetActive(false);

            if (_parent != null)
                item.transform.SetParent(_parent);

            _freeItems.Add(item);
        }

        private void CreateFirstItems()
        {
            _freeItems = new List<T>(_initialCount);

            for (int i = 0; i < _initialCount; i++)
            {
                PushItem(CreateNewItem());
            }
        }

        private T CreateNewItem()
        {
            T newItem = _factory.CreateNewItem();

            newItem.Released.Add(_onReleasedAction);

            if (_parent != null)
                newItem.transform.SetParent(_parent);

            return newItem;
        }
    }
}