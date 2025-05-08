using Cysharp.Threading.Tasks;
using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Gameplay.Services.Score;
using G2048_3D.Pool;
using G2048_3D.Services.AssetProvider;
using G2048_3D.Services.ConfigsProvider;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeService
    {
        private readonly CubeSpawner _spawner;

        private IAssetProvider _assetProvider;
        private readonly IScoreChangerService _scoreChanger;
        private readonly IMergeService<MergableCubeData, MergableCubeVisual> _mergeService;
        public CubeSpawner Spawner => _spawner;

        public CubeService(IAssetProvider assetProvider, IConfigsProvider configsProvider,
            IScoreChangerService scoreChanger, IMergeService<MergableCubeData, MergableCubeVisual> mergeService, MonoRegisteredObjectsFactory objectsFactory)
        {
            _assetProvider = assetProvider;
            _scoreChanger = scoreChanger;
            _mergeService = mergeService;

            _spawner = new CubeSpawner(_assetProvider, objectsFactory, configsProvider);
            _spawner.PushedCubeSpawned += CubeSpawned;
            _spawner.CubeSpawned += CubeSpawned;

        }

        private void CubeSpawned(MergableCube cube)
        {
            cube.PushCompleted += CubePushCompleted;
            cube.CanBeMerged += Merge;
            cube.Released.Add(CubeDespawned);
        }

        private void CubePushCompleted(MergableCube cube)
        {
            cube.IsPushableTarget = false;
            cube.PushCompleted -= CubePushCompleted;

        }

        private void CubeDespawned(MergableCube cube)
        {
            cube.CanBeMerged -= Merge;
            cube.Released.Remove(CubeDespawned);
        }
        private void SpawnMergedCube(MergableCubeData cubeData)
        {
            _spawner.SpawnMergedCube(cubeData);
        }

        public void Initialize(Transform spawnPoint) =>
            _spawner.SetSpawnPoint(spawnPoint);

        public void SpawnPushCube()
        {
            MergableCubeData cubeData = new(2, Vector3.zero, Vector3.zero);
            _spawner.SpawnCube(cubeData);
        }
        private void Merge(MergableCube first, MergableCube second)
        {
            MergeAsync(first, second);
        }
        public async UniTask MergeAsync(MergableCube first, MergableCube second)
        {
            first.CanBeMerged -= Merge;
            second.CanBeMerged -= Merge;

            MergableData<MergableCubeData, MergableCubeVisual> mergableData = new(first.Data, second.Data, first.Visual, second.Visual);
            MergeResult<MergableCubeData> mergeResult = await _mergeService.TryMergeAsync(mergableData);

            if (mergeResult != null && mergeResult.Success)
            {
                _scoreChanger.ChangeScore(first.Level / 2);
                SpawnMergedCube(mergeResult.Result);
                first.Release();
                second.Release();
            }
            else
            {
                first.CanBeMerged += Merge;
                second.CanBeMerged += Merge;

            }

        }
    }
}
