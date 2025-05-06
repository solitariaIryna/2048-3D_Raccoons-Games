using G2048_3D.Services.Command;
using UnityEngine;

namespace G2048_3D.Gameplay.Commands.Parameters
{
    public class CmdCreateCubeParameters : ICommandParameter
    {
        public readonly int Number;
        public readonly Vector3 Position;

        public CmdCreateCubeParameters(int number, Vector3 position)
        {
            Number = number;
            Position = position;
        }
    }
}
