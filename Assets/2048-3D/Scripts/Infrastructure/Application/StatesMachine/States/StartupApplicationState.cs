using G2048_3D.Infrastructure.StateMachine;
using G2048_3D.Services.ConfigsProvider;
using UnityEngine;

namespace G2048_3D.Infrastructure.App.StatesMachine
{
    public class StartupApplicationState : IState
    {
        private readonly IConfigsProvider _configsProvider;

        public StartupApplicationState(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void Enter()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Input.multiTouchEnabled = false;

            _configsProvider.LoadAll();
        }

        public void Exit()
        {
        }
    }    



}
