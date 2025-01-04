using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
    public class LoadingHomeScreenState : IState
    {
        private const string HomeScreenSceneName = "HomeScreen";
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;

        public LoadingHomeScreenState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(HomeScreenSceneName, EnterHomeScreenState);
        }

        public void Exit()
        {
        }

        private void EnterHomeScreenState()
        {
            _stateMachine.Enter<HomeScreenState>();
        }
    }
}