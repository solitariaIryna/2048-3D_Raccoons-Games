using G2048_3D.Configs.Cube;

namespace G2048_3D.Services.ConfigsProvider
{
    public interface IConfigsProvider
    {
        CubeConfig GetCubeConfig();
        void LoadAll();
    }
}