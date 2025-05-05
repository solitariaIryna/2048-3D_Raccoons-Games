namespace G2048_3D.Services.Command
{
    public interface ICommandWithResult<TParams, TResult>
    {
        CommandResult<TResult> Execute(TParams parameters);
    }
}