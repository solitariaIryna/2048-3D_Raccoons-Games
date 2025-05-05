namespace G2048_3D.Services.Command
{
    public class CommandResult<TResult>
    {
        public bool Success { get; }
        public TResult Result { get; }

        public CommandResult(bool success, TResult result)
        {
            Success = success;
            Result = result;
        }

        public static CommandResult<TResult> Succeeded(TResult result) =>
            new CommandResult<TResult>(true, result);

        public static CommandResult<TResult> Failed() =>
            new CommandResult<TResult>(false, default);
    }
}