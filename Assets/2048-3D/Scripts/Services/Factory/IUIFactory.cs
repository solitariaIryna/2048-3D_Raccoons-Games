using G2048_3D.UI;

namespace G2048_3D.Services.Factory
{
    public interface IUIFactory
    {
        T CreateWindow<T>(string path) where T : BaseDisplay;
    }
}