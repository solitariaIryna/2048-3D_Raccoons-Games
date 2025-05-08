using G2048_3D.Services.AssetProvider;
using G2048_3D.UI;

namespace G2048_3D.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        private UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public T CreateWindow<T>(string path) where T : BaseDisplay
        {
            T window = _assetProvider.Instantiate<T>(path);

            return window;
        }
    }
}
