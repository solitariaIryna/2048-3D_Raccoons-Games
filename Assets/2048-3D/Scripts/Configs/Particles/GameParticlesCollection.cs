using G2048_3D.Gameplay.Services.Particle;
using UnityEngine;

namespace G2048_3D.Configs.Particles
{
    [CreateAssetMenu(fileName = nameof(GameParticlesColection), menuName = "Configs/Particles/" + nameof(GameParticlesColection))]
    public class GameParticlesColection : ScriptableObject
    {
        [field: SerializeField] public ParticleData[] Particles { get; private set; }
    }
}
