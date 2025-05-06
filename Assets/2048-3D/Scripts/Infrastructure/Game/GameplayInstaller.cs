using G2048_3D.Services.Command;
using G2048_3D.Infrastructure.Game.Factory;
using G2048_3D.Infrastructure.Gameplay.StatesMachine;
using Zenject;
using G2048_3D.Gameplay.Services;
using G2048_3D.Gameplay.Services.Input;
using G2048_3D.Pool;

namespace G2048_3D.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ICommandProcessor>()
                .To<CommandProcessor>()
                .AsSingle();

            Container
                .Bind<IInputService>()
                .To<HybridInputService>()
                .AsSingle();

            Container
                 .BindInterfacesAndSelfTo<MonoRegisteredObjectsFactory>()
                 .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<LevelsService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CubeService>()
                .AsSingle();

            Container
               .Bind<GameplayStatesFactory>()
               .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameplayStateMachine>()
                .AsSingle();

           
        }
    }
}
