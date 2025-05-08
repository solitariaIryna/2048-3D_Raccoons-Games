using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Pool;
using G2048_3D.Services.AssetProvider;
using G2048_3D.Services.ConfigsProvider;
using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeSpawner
    {
        private const string CUBE_POOL_NAME = "Cube Pool";

        private readonly IAssetProvider _assetProvider;
        private readonly MonoRegisteredObjectsFactory _objectsFactory;
        private readonly IConfigsProvider _configsProvider;
        private MonoQueuePool<MergableCube> _cubePool;

        private Transform _cubeParent;

        private Transform _spawnPoint;

        public Action<MergableCube> PushedCubeSpawned;
        public Action<MergableCube> CubeSpawned;
        public Action CubePushedEnded;
        public CubeSpawner(IAssetProvider assetProvider, 
            MonoRegisteredObjectsFactory objectsFactory, IConfigsProvider configsProvider)
        {
            _assetProvider = assetProvider;
            _objectsFactory = objectsFactory;
            _configsProvider = configsProvider;
            _cubeParent = new GameObject(CUBE_POOL_NAME).transform;
            CreateCubePool();
        }
        public void SetSpawnPoint(Transform spawnPoint) => 
            _spawnPoint = spawnPoint;
        public void SpawnCube(MergableCubeData cubeData)
        {
            MergableCube cube = _cubePool.GetItem();
            cube.IsPushableTarget = true;
            cube.SetData(cubeData);
            cube.transform.eulerAngles = Vector3.zero;
            cube.Position = _spawnPoint.position;
            cube.PushCompleted += OnCubeCollisitionHappen;
            cube.Released.Add(CubeDespawned);
            cube.SimpleShow();
            PushedCubeSpawned?.Invoke(cube);
        }

        private void CubeDespawned(MergableCube cube)
        {
            cube.Released.Remove(CubeDespawned);
            cube.PushCompleted -= OnCubeCollisitionHappen;
        }

        private void OnCubeCollisitionHappen(MergableCube cube)
        {
            cube.IsPushableTarget = false;
            cube.PushCompleted -= OnCubeCollisitionHappen;
            MergableCubeData newData = new(_configsProvider.GetCubeConfig().GetNumber(), _spawnPoint.position, Vector3.zero);
            SpawnCube(newData);
        }

        public void SpawnMergedCube(MergableCubeData cubeData)
        {
            MergableCube cube = _cubePool.GetItem();           
            cube.SetData(cubeData);
            cube.transform.eulerAngles = cubeData.Rotation;
            cube.Pushable.EnablePhysics(true);
            cube.PushShow();

            CubeSpawned?.Invoke(cube);
        }
        private void CreateCubePool()
        {
            MergableCube prefab = _assetProvider.Load<MergableCube>(AssetPath.CubePath);

            _cubePool = new(prefab, 6, _cubeParent, true);

            _cubePool.Initialize(_objectsFactory);
        }
    }
}
