using Zenject;

namespace G2048_3D.Infrastructure
{
    public class GameRunner : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameInitializer>()
                .AsSingle();
        }
    }
}
