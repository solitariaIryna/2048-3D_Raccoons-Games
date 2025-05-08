using G2048_3D.Configs.Cube;
using G2048_3D.Gameplay.Services.Particle;

namespace G2048_3D.Services.ConfigsProvider
{
    public interface IConfigsProvider
    {
        ParticleData[] ForParticles();
        CubeConfig GetCubeConfig();
        void LoadAll();
    }
}