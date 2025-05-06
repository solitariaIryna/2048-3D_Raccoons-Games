using G2048_3D.Configs.Cube;
using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Services.AssetProvider;
using G2048_3D.Services.ConfigsProvider;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeService
    {
        private readonly CubeSpawner _spawner;
        private readonly CubeMerger _merger;

        private IAssetProvider _assetProvider;

        private CubeConfig _cubeConfig;

        public CubeSpawner Spawner => _spawner;
        public CubeService(IAssetProvider assetProvider, IConfigsProvider configsProvider)
        {
            _assetProvider = assetProvider;
            _cubeConfig = configsProvider.GetCubeConfig();

            _spawner = new CubeSpawner(_assetProvider);
            _merger = new CubeMerger();

            _spawner.PushedCubeSpawned += _merger.RegisterCube;
            _spawner.CubePushedEnded += SpawnCube;
            _merger.MergeHappened += SpawnMergedCube;
        }

        private void SpawnMergedCube(int number, Vector3 position)
        {
            CubeData cubeData = new(number, position, _cubeConfig);
            _spawner.SpawnMergedCube(cubeData);
        }

        public void Initialize(Transform spawnPoint) => 
            _spawner.SetSpawnPoint(spawnPoint);

        public void SpawnCube()
        {
            CubeData cubeData = new(2, Vector3.zero, _cubeConfig);
            _spawner.SpawnCube(cubeData);
        }
    }
}
