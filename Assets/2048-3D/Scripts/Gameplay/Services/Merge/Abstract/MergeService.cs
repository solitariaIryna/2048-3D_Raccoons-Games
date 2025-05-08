using Cysharp.Threading.Tasks;

namespace G2048_3D.Gameplay.Services
{
    public class MergeService<TData, TVisual> : IMergeService<TData, TVisual>
    {
        private readonly IMergeStrategy<TData, TVisual> _mergeStrategy;

        public MergeService(IMergeStrategy<TData, TVisual> mergeStrategy)
        {
            _mergeStrategy = mergeStrategy;
        }

        public async UniTask<MergeResult<TData>> TryMergeAsync(MergableData<TData, TVisual> data)
        {
            MergeResult<TData> result = null;

            if (_mergeStrategy.CanMerge(data))
            {
                result = await _mergeStrategy.Merge(data);
                return result;
            }
            return result;
        }

    }
}
