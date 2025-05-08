using UnityEngine;

namespace G2048_3D.Gameplay.Entities.Cube
{
    public class MergableCubeData
    {
        public int Level { get; private set; }
        public Vector3 Position;
        public Vector3 Rotation;

        public MergableCubeData(int level, Vector3 position, Vector3 rotation) 
        {
            Level = level;
            Position = position;
            Rotation = rotation;
        }
    }
}
