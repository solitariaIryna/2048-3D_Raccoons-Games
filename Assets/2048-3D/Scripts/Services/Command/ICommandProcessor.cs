namespace G2048_3D.Services.Command
{
    public interface ICommandProcessor
    {
        CommandResult<TResult> Process<TCommandParams, TResult>(TCommandParams comandParams) where TCommandParams : ICommandParameter;
        bool Process<TCommandParams>(TCommandParams comandParams) where TCommandParams : ICommandParameter;
        void RegisterCommand<TParameter>(ICommand<TParameter> command) where TParameter : ICommandParameter;
        void RegisterCommand<TCommandParams, TResult>(ICommandWithResult<TCommandParams, TResult> command) where TCommandParams : ICommandParameter;
    }
}