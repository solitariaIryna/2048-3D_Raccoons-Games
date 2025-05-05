using G2048_3D.Infrastructure.StateMachine;
using G2048_3D.Core;

namespace G2048_3D.Infrastructure.App.StatesMachine
{
    public class LoadingSceneApplicationState : IPayloadState<LoadSceneParameters>
    {
        private SceneLoader _sceneLoader;

        public LoadingSceneApplicationState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(LoadSceneParameters parameters)
        {
           LoadScene(parameters);
        }

        public void Exit()
        {

        }

        private void LoadScene(LoadSceneParameters parameters)
        {
            _sceneLoader.Load(parameters);
        }
    }

}


