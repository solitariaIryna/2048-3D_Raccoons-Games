using G2048_3D.Configs.Cube;
using UnityEngine;

namespace G2048_3D.Gameplay.Entities.Cube
{
    public class CubeData
    {
        public int Number;
        public Vector3 Position;
        public CubeConfig Config;

        public CubeData(int number, Vector3 position, CubeConfig config)
        {
            Number = number;
            Position = position;
            Config = config;
        }
    }
}
