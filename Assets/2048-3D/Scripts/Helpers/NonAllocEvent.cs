using System.Collections.Generic;
using System;

namespace G2048_3D.Helpers
{
    public class NonAllocEvent<T1>
    {
        private List<Action<T1>> _actions = new List<Action<T1>>();

        public int SubscribersCount => _actions.Count;

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value);
        }

        public void InvokeSafe(T1 value)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                if (i < _actions.Count)
                    _actions[i](value);
            }
        }

        public void Add(Action<T1> action) => _actions.Add(action);

        public void Remove(Action<T1> action) => _actions.Remove(action);
    }
}
