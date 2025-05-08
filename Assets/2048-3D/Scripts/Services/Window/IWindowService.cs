using G2048_3D.UI;

namespace G2048_3D.Services.Window
{
    public interface IWindowService
    {
        T OpenWindow<T>(WindowType id) where T : BaseDisplay;
    }
}