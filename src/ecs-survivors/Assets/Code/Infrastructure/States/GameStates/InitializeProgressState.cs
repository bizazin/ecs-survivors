using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
    public class InitializeProgressState : IState
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IGameStateMachine _stateMachine;

        public InitializeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
        }

        public void Enter()
        {
            InitializeProgress();

            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        public void Exit()
        {
        }

        private void InitializeProgress()
        {
            CreateNewProgress();
        }

        private void CreateNewProgress()
        {
            _progressProvider.SetProgressData(new ProgressData());
        }
    }
}