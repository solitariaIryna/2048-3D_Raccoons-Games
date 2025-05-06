using G2048_3D.Gameplay.Entities.Cube;
using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public class CubeMerger
    {
        public Action<int, Vector3> MergeHappened;
        public void RegisterCube(CubeEntity cube)
        {
            cube.CanBeMerged += MergeCubes;
        }

        public void MergeCubes(CubeEntity first, CubeEntity second)
        {
            if (first.Number == second.Number)
            {
                int newNumber = first.Number + second.Number;

                first.Release();
                first.CanBeMerged -= MergeCubes;
                second.Release();
                second.CanBeMerged -= MergeCubes;

                MergeHappened?.Invoke(newNumber, second.Position);
            }

        }
    }
}
