using G2048_3D.Services.AssetProvider;

namespace G2048_3D.Services.ConfigsProvider
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAssetProvider _assetProvider;
        public ConfigsProvider(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void LoadAll()
        {
           // GameConfig = _assetProvider.Load<GameConfig>("Configs/GameConfig");
          //  await UniTask.WaitUntil(() => GameConfig != null);
        }

    }
}
