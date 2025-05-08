using Cysharp.Threading.Tasks;
using G2048_3D.Configs.Cube;
using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Services.ConfigsProvider;
using System.Threading;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeMergeStrategy : IMergeStrategy<MergableCubeData, MergableCubeVisual>
    {
        private CubeConfig _cubeConfig;
        public CubeMergeStrategy(IConfigsProvider configsProvider) =>
            _cubeConfig = configsProvider.GetCubeConfig();
        public bool CanMerge(MergableData<MergableCubeData, MergableCubeVisual> data) =>
            data.First.Level == data.Second.Level;

        public async UniTask<MergeResult<MergableCubeData>> Merge(MergableData<MergableCubeData, MergableCubeVisual> data)
        {
            MergableCubeData first = data.First;
            MergableCubeData second = data.Second;

            int newNumber = first.Level + second.Level;

            Vector3 avarangePosition = (first.Position + second.Position) / 2;

            Vector3 directionToSecond = (second.Position - first.Position).normalized;
            Vector3 directionToFirst = (first.Position - second.Position).normalized;

            Quaternion firstLookRotation = Quaternion.LookRotation(directionToSecond);
            Quaternion secondLookRotation = Quaternion.LookRotation(directionToFirst);

            Vector3 firstTargetRotation = firstLookRotation.eulerAngles;
            Vector3 secondTargetRotation = secondLookRotation.eulerAngles;

            Color color = _cubeConfig.GetColorForNumber(newNumber);

            await UniTask.WhenAll(
                data.VisualFirst.PlayMergeAnimation(avarangePosition, color, firstTargetRotation, new CancellationTokenSource()),
                data.VisualSecond.PlayMergeAnimation(avarangePosition, color, secondTargetRotation, new CancellationTokenSource())
            );
            Taptic.Success();
            MergableCubeData newData = new MergableCubeData(newNumber, avarangePosition, firstTargetRotation);

            MergeResult<MergableCubeData> result = new(true, newData);
            return result;


        }

    }
}
