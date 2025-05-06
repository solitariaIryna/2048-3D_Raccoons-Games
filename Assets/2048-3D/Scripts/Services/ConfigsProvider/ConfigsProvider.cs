using G2048_3D.Configs.Cube;
using G2048_3D.Services.AssetProvider;

namespace G2048_3D.Services.ConfigsProvider
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAssetProvider _assetProvider;

        private CubeConfig _cubeConfig;
        public ConfigsProvider(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void LoadAll()
        {
            LoadCubeConfig();
        }

        public void LoadCubeConfig() => 
            _cubeConfig = _assetProvider.Load<CubeConfig>("Configs/CubeConfig");
        public CubeConfig GetCubeConfig()
        {
            return _cubeConfig;
        }

    }
}
