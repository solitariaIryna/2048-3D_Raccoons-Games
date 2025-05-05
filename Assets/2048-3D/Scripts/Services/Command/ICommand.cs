
namespace G2048_3D.Services.Command
{
    public interface ICommand<TParameter> where TParameter : ICommandParameter
    {
        bool Execute(TParameter parameters);
    }

}
