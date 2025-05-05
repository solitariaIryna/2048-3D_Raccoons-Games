using G2048_3D.Services.Command;
using G2048_3D.Infrastructure.Game.Factory;
using G2048_3D.Infrastructure.Gameplay.StatesMachine;
using Zenject;

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
                .BindInterfacesAndSelfTo<GameFactory>()
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
