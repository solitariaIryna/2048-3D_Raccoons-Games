using G2048_3D.Configs.Cube;
using G2048_3D.Configs.Particles;
using G2048_3D.Gameplay.Services.Particle;
using G2048_3D.Services.AssetProvider;

namespace G2048_3D.Services.ConfigsProvider
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAssetProvider _assetProvider;

        private CubeConfig _cubeConfig;
        private GameParticlesColection _particlesCollection;
        public ConfigsProvider(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void LoadAll()
        {
            LoadCubeConfig();
            LoadParticles();
        }

        public void LoadCubeConfig() => 
            _cubeConfig = _assetProvider.Load<CubeConfig>("Configs/CubeConfig");

        public void LoadParticles() =>
            _particlesCollection = _assetProvider.Load<GameParticlesColection>("Configs/Particles/GameParticlesColection");

        public CubeConfig GetCubeConfig() =>
            _cubeConfig;

        public ParticleData[] ForParticles() =>
            _particlesCollection.Particles;
    }
}
