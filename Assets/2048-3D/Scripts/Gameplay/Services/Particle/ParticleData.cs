using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Services.Particle
{
    [Serializable]
    public class ParticleData
    {
        [field: SerializeField] public ParticleType Type { get; private set; }
        [field: SerializeField] public ParticleSystem Prefab { get; private set; }
        [field: SerializeField] public int Count { get; private set; } = 5;
    }
}
