using G2048_3D.Services.AssetProvider;
using G2048_3D.Services.Factory;
using G2048_3D.UI;
using System.Collections.Generic;
using UnityEngine;

namespace G2048_3D.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uIFactory;

        private Dictionary<WindowType, string> _windowPaths;

        private List<BaseDisplay> _openedWindows = new();
        public WindowService(IUIFactory uIFactory)
        {
            _uIFactory = uIFactory;

            _windowPaths = new Dictionary<WindowType, string>()
            {
                { WindowType.Lose, AssetPath.LoseUIPath }

            };
        }

        public T OpenWindow<T>(WindowType id) where T : BaseDisplay
        {
            T window = _uIFactory.CreateWindow<T>(GetWindowPath(id));
            _openedWindows.Add(window);
            return window;
        }
        private string GetWindowPath(WindowType id)
        {
            if (!_windowPaths.ContainsKey(id))
            {
                Debug.LogError($"AssetData not found for ID: {id}");
                return null;
            }

            return _windowPaths[id];
        }
        public void Dispose()
        {
            foreach (BaseDisplay window in _openedWindows)
                window.Deactivate();

            _openedWindows.Clear();
        }
    }
}
