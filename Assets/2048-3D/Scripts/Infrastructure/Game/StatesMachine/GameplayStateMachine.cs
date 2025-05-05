using G2048_3D.Infrastructure.StateMachine;

namespace G2048_3D.Infrastructure.Gameplay.StatesMachine
{
    public class GameplayStateMachine : BaseStateMachine
    {
        private GameplayStatesFactory _gameplayStatesFactory;

        public GameplayStateMachine(GameplayStatesFactory gameplayStatesFactory)
        {
            _gameplayStatesFactory = gameplayStatesFactory;
        }

        public override void Initialize()
        {
            _states = _gameplayStatesFactory.CreateStates();
        }
    }
}
