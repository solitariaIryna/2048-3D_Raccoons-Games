using G2048_3D.Gameplay.Levels;
using G2048_3D.Gameplay.Services;
using G2048_3D.Infrastructure.Game.Factory;
using G2048_3D.Infrastructure.StateMachine;
using G2048_3D.Services.Command;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class LoadLevelState : IState
    {
        private readonly GameFactory _gameFactory;
        private readonly ICommandProcessor _commandProcessor;

        private readonly LevelsService _levelsService;
        private readonly CubeService _cubeService;


        public LoadLevelState(GameFactory gameFactory, ICommandProcessor commandProcessor, LevelsService levelsService,
            CubeService cubeService)
        {
            _gameFactory = gameFactory;
            _commandProcessor = commandProcessor;
            _levelsService = levelsService;
            _cubeService = cubeService;
        }

        public void Enter()
        {
            RegisterGameplayCommands();
          //  CinemachineCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineCamera>();

            InitWorld();
        }

        private void InitWorld()
        {
            Level level = _levelsService.CreateLevel(1);
            _cubeService.Initialize(level.CubeSpawnPoint);
            _cubeService.SpawnCube();
        }

        private void RegisterGameplayCommands()
        {

        }

        public void Exit()
        {

        }
    }        
}
