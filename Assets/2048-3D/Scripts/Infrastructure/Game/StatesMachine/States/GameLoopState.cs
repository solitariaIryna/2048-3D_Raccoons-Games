using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Gameplay.Levels;
using G2048_3D.Gameplay.Services;
using G2048_3D.Infrastructure.StateMachine;
using UnityEngine;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class GameLoopState : IState
    {
        private readonly GameplayStateMachine _stateMachine;
        private readonly LevelsService _levelsService;

        public GameLoopState(GameplayStateMachine stateMachine, LevelsService levelsService)
        {
            _stateMachine = stateMachine;
            _levelsService = levelsService;
        }

        public void Enter()
        {
            Level level = _levelsService.CurrentLevel;

            level.DeadArea.TriggerEnter += DeadAreaTriggered;
        }

        private void DeadAreaTriggered(Collider collider)
        {
            if (collider.TryGetComponent<MergableCube>(out MergableCube cube))
            {
                if (cube.IsPushableTarget)
                    return;

                Taptic.Failure();
                _levelsService.CurrentLevel.DeadArea.TriggerEnter -= DeadAreaTriggered;
                _stateMachine.Enter<LoseGameState>();
            }
            
        }

        public void Exit()
        {

        }
    }
}
