using G2048_3D.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace G2048_3D.Pool
{
    public class MonoQueuePool<T> where T : MonoBehaviour, IReleaseable<T>, IResetable
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialCount = 16;
        [Space]
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _disableEnableItems = true;

        private IFactory _factory;
        private Queue<T> _freeItems;
        private HashSet<T> _inPoolSet;

        private readonly object _lock = new object();

        private Action<T> _onReleasedAction;

        public T Prefab => _prefab;

        public MonoQueuePool(T prefab, int initialCount, Transform parent, bool disableEnableItems)
        {
            _prefab = prefab;
            _initialCount = initialCount;
            _parent = parent;
            _disableEnableItems = disableEnableItems;
        }

        public MonoQueuePool() : this(null, GameConstants.DEFAULT_POOL_ELEMENTS_COUNT, null, true) { }

        public void Initialize(IFactory factory)
        {
            _onReleasedAction = ReleaseItem;
            _factory = factory;

            CreateInitialItems();
        }

        public T GetItem()
        {
            T item;

            if (_freeItems.Count > 0)
            {
                item = _freeItems.Dequeue();
                _inPoolSet.Remove(item); 
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

        private void ReleaseItem(T item)
        {
            lock (_lock)
            {
                if (_inPoolSet.Contains(item))
                {
#if UNITY_EDITOR
                    Debug.LogError($"Duplicated Item Detected! {item.name}", item);
#endif
                    return;
                }

                if (_disableEnableItems)
                    item.gameObject.SetActive(false);

                if (_parent != null)
                    item.transform.SetParent(_parent);

                _freeItems.Enqueue(item);
                _inPoolSet.Add(item);
            }
        }


        private void CreateInitialItems()
        {
            _freeItems = new Queue<T>(_initialCount);
            _inPoolSet = new HashSet<T>();

            for (int i = 0; i < _initialCount; i++)
            {
                ReleaseItem(CreateNewItem());
            }
        }

        private T CreateNewItem()
        {
            T newItem = _factory.CreateObject<T>(_prefab);

            newItem.Released.Add(_onReleasedAction);

            if (_parent != null)
                newItem.transform.SetParent(_parent);

            return newItem;
        }
    }    
}