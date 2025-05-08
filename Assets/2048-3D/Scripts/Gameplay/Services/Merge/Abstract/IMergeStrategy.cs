using Cysharp.Threading.Tasks;

namespace G2048_3D.Gameplay.Services
{
    public interface IMergeStrategy<TData, TVisual>
    {
        bool CanMerge(MergableData<TData, TVisual> data);
        UniTask<MergeResult<TData>> Merge(MergableData<TData, TVisual> data);
    }
}
