using Cysharp.Threading.Tasks;

namespace G2048_3D.Gameplay.Services
{
    public interface IMergeService<TData, TVisual>
    {
        UniTask<MergeResult<TData>> TryMergeAsync(MergableData<TData, TVisual> data);
    }
}