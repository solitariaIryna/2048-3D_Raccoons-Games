using G2048_3D.Gameplay.Levels;
using G2048_3D.Gameplay.Services;
using G2048_3D.Infrastructure.StateMachine;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class LoadLevelState : IState
    {
        private readonly GameplayStateMachine _stateMachine;

        private readonly LevelsService _levelsService;
        private readonly CubeService _cubeService;


        public LoadLevelState(GameplayStateMachine stateMachine, LevelsService levelsService,
            CubeService cubeService)
        {
            _stateMachine = stateMachine;
            _levelsService = levelsService;
            _cubeService = cubeService;
        }

        public void Enter()
        {
            InitWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitWorld()
        {
            Level level = _levelsService.CreateLevel(1);
            _cubeService.Initialize(level.CubeSpawnPoint);
            _cubeService.SpawnPushCube();
        }

        public void Exit()
        {

        }
    }        
}
