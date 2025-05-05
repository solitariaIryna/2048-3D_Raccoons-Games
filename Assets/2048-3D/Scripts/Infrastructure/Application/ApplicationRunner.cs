using G2048_3D.Constants;
using G2048_3D.Core;
using G2048_3D.Infrastructure.App.StatesMachine;
using UnityEngine;
using Zenject;

namespace G2048_3D.Infrastructure.App
{
    public class ApplicationRunner : MonoBehaviour
    {
        private ApplicationStateMachine _applicationStateMachine;

        [Inject]
        private void Construct(ApplicationStateMachine applicationStateMachine)
        {
            _applicationStateMachine = applicationStateMachine;
        }
        private void Start()
        {
            _applicationStateMachine.Initialize();
            Run();
        }
        private void Run()
        {
            var loadBootSceneParameters = new LoadSceneParameters(ApplicationConstants.BOOT_SCENE, false, onLoaded: () =>
            {
                _applicationStateMachine.Enter<StartupApplicationState>();

                var loadGameSceneParameters = new LoadSceneParameters(ApplicationConstants.GAME_SCENE, true, onLoaded: () =>
                {
                    _applicationStateMachine.Enter<GameApplicationState>();
                });
                _applicationStateMachine.Enter<LoadingSceneApplicationState, LoadSceneParameters>(loadGameSceneParameters);
            });

            _applicationStateMachine.Enter<LoadingSceneApplicationState, LoadSceneParameters>(loadBootSceneParameters);


        }
    }
}
