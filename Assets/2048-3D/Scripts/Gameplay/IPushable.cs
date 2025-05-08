
using System;
using UnityEngine;

namespace G2048_3D.Gameplay
{
    public interface IPushable
    {
        Vector3 Position { get; set; }
        event Action Pushed;
        event Action PushCompleted;

        void EnablePhysics(bool value);
        void Push(Vector3 force);
        void ResetPhysics();
    }
}
