using UnityEngine;

namespace G2048_3D.Gameplay
{
    public interface IPushable
    {
        Vector3 Position { get; set; }
        void Push(float force);
    }
}
