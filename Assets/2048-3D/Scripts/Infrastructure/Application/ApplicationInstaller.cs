using G2048_3D.Core;
using G2048_3D.Infrastructure.App.StatesMachine;
using G2048_3D.Services.AssetProvider;
using G2048_3D.Services.ConfigsProvider;
using G2048_3D.Services.Factory;
using G2048_3D.Services.Window;
using Zenject;

namespace G2048_3D.Infrastructure.App
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
               .Bind<ApplicationStatesFactory>()
               .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ApplicationStateMachine>()
                .AsSingle();

            Container
                .Bind<IAssetProvider>()
                .To<ResourcesAssetProvider>()
                .AsSingle();

            Container
                .Bind<IConfigsProvider>()
                .To<ConfigsProvider>()
                .AsSingle();

            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

            Container
                .Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle();

        }
    }
}
