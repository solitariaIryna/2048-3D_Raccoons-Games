using G2048_3D.Infrastructure.StateMachine;

namespace G2048_3D.Infrastructure.App.StatesMachine
{
    public class ApplicationStateMachine : BaseStateMachine
    {
        private ApplicationStatesFactory _applicationStatesFactory;

        public ApplicationStateMachine(ApplicationStatesFactory applicationStatesFactory)
        {
            _applicationStatesFactory = applicationStatesFactory;
        }

        public override void Initialize()
        {
            _states = _applicationStatesFactory.CreateStates();
        }
    }

}
