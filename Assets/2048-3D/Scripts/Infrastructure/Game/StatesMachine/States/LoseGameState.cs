using G2048_3D.Constants;
using G2048_3D.Core;
using G2048_3D.Infrastructure.StateMachine;
using G2048_3D.Services.Window;
using G2048_3D.UI;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class LoseGameState : IState
    {
        private readonly GameplayStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly SceneLoader _sceneLoader;

        public LoseGameState(GameplayStateMachine stateMachine, IWindowService windowService, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            LoseUI loseUI = _windowService.OpenWindow<LoseUI>(WindowType.Lose);
            loseUI.Construct(RestartLevel);
        }

        private void RestartLevel()
        {
            LoadSceneParameters loadSceneParameters = new LoadSceneParameters
                (ApplicationConstants.GAME_SCENE, true);

            _sceneLoader.Load(loadSceneParameters);
        }

        public void Exit()
        {
        }
    }    
}
