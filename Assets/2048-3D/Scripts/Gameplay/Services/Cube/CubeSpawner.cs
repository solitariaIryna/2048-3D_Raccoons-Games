using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Pool;
using G2048_3D.Services.AssetProvider;
using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeSpawner
    {
        private const string CUBE_POOL_NAME = "Cube Pool";

        private readonly IAssetProvider _assetProvider;
        private MonoStackPool<CubeEntity> _cubePool;

        private Transform _cubeParent;

        private Transform _spawnPoint;

        public Action<CubeEntity> PushedCubeSpawned;
        public Action<CubeEntity> CubeSpawned;
        public Action CubePushedEnded;
        public CubeSpawner(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _cubeParent = new GameObject(CUBE_POOL_NAME).transform;
            CreateCubePool();
        }
        public void SetSpawnPoint(Transform spawnPoint) => 
            _spawnPoint = spawnPoint;
        public void SpawnCube(CubeData cubeData)
        {
            CubeEntity cube = _cubePool.GetItem();
            cube.Position = _spawnPoint.position;
            cube.SetData(cubeData);
            cube.PushEnded += OnCubeCollisitionEntered;
            cube.Released.Add(CubeDespawned);
            PushedCubeSpawned?.Invoke(cube);
        }

        private void CubeDespawned(CubeEntity cube)
        {
            cube.PushEnded?.Invoke(cube);
            cube.PushEnded -= OnCubeCollisitionEntered;
        }

        public void SpawnMergedCube(CubeData cubeData)
        {
            CubeEntity cube = _cubePool.GetItem();
            cube.SetData(cubeData);
            cube.EnablePhysics();

            CubeSpawned?.Invoke(cube);
        }

        private void OnCubeCollisitionEntered(CubeEntity cube)
        {
            cube.PushEnded -= OnCubeCollisitionEntered;
            CubePushedEnded?.Invoke();
        }

        private void CreateCubePool()
        {
            CubeEntity prefab = _assetProvider.Load<CubeEntity>(AssetPath.CubePath);

            _cubePool = new(prefab, 0, _cubeParent, true);

            MonoIInitializableFactory<CubeEntity> factory = new(prefab, null);

            _cubePool.Initialize(factory);
        }
    }
}
