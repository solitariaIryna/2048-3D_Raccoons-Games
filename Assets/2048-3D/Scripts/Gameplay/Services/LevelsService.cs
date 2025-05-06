
using G2048_3D.Gameplay.Levels;
using G2048_3D.Services.AssetProvider;

namespace G2048_3D.Gameplay.Services
{
    public class LevelsService
    {
        private readonly IAssetProvider _assetProvider;

        public LevelsService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public Level CreateLevel(int number)
        {
            Level level = _assetProvider.Instantiate<Level>(AssetPath.LevelPath);
            return level;
        }
    }
}
