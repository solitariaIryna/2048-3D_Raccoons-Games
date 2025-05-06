using G2048_3D.Gameplay.Commands.Parameters;
using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Services.Command;

namespace G2048_3D.Gameplay.Commands
{
    public class CmdCreateCube : ICommandWithResult<CmdCreateCubeParameters, CubeEntity>
    {
        public CommandResult<CubeEntity> Execute(CmdCreateCubeParameters parameters)
        {
            return null;
        }
    }
}
