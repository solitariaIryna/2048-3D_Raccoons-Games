using G2048_3D.Infrastructure.Gameplay.StatesMachine;
using Zenject;
using G2048_3D.Gameplay.Services;
using G2048_3D.Gameplay.Services.Score;
using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Pool;
using G2048_3D.Gameplay.Services.Particle;
using UnityEngine;

namespace G2048_3D.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GameObject inputObject = new GameObject("Input Service");

            IInputService inputService = Application.isMobilePlatform
                                       ? inputObject.AddComponent<MobileInputService>()
                                       : inputObject.AddComponent<StandaloneInputService>();

            Container
                .Bind<IInputService>()
                .FromInstance(inputService)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ParticleRunnerService>()
                .AsSingle();

            Container
                .Bind<IScoreChangerService>()
                .To<ScoreChangerService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<LevelsService>()
                .AsSingle();


            Container
                .Bind<IMergeStrategy<MergableCubeData, MergableCubeVisual>>()
                .To<CubeMergeStrategy>()
                .AsSingle();

            Container
                .Bind<IMergeService<MergableCubeData, MergableCubeVisual>>()
                .To<MergeService<MergableCubeData, MergableCubeVisual>>()
                .AsSingle();

            Container
                .Bind<MonoRegisteredObjectsFactory>()
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
