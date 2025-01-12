using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.SaveLoad;

namespace Code.Infrastructure.States.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameStateMachine _stateMachine;

        public LoadProgressState(
            IGameStateMachine stateMachine,
            ISaveLoadService saveLoadService,
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            InitializeProgress();

            _stateMachine.Enter<ActualizeProgressState>();
        }

        private void InitializeProgress()
        {
            if (_saveLoadService.HasSavedProgress)
                _saveLoadService.LoadProgress();
            else
                CreateNewProgress();
        }

        public void Exit()
        {
        }

        private void CreateNewProgress()
        {
            _saveLoadService.CreateProgress();
            
            CreateMetaEntity.Empty()
                .With(x => x.isStorage = true)
                .AddGold(0)
                .AddGoldPerSecond(_staticDataService.AfkGainConfig.GoldPerSecond);
        }
    }
}