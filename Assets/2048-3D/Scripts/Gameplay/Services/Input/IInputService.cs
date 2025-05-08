using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        event Action MouseUp;
    }
}
