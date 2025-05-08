namespace G2048_3D.Gameplay.Services
{
    public class MergeResult<TResult>
    {
        public bool Success { get; }
        public TResult Result { get; }

        public MergeResult(bool success, TResult result)
        {
            Success = success;
            Result = result;
        }
        public static MergeResult<TResult> Succeeded(TResult result) =>
            new MergeResult<TResult>(true, result);

        public static MergeResult<TResult> Failed() =>
            new MergeResult<TResult>(false, default);
    }
}
