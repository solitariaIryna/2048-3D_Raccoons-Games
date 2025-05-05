using G2048_3D.Infrastructure.Game.Factory;
using G2048_3D.Infrastructure.StateMachine;
using G2048_3D.Services.Command;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class LoadLevelState : IState
    {
        private readonly GameFactory _gameFactory;
        private readonly ICommandProcessor _commandProcessor;


        public LoadLevelState(GameFactory gameFactory, ICommandProcessor commandProcessor)
        {
            _gameFactory = gameFactory;
            _commandProcessor = commandProcessor;
        }

        public void Enter()
        {
            RegisterGameplayCommands();
          //  CinemachineCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineCamera>();

            InitWorld();
        }

        private void InitWorld()
        {

        }

        private void RegisterGameplayCommands()
        {

        }

        public void Exit()
        {

        }
    }        
}
