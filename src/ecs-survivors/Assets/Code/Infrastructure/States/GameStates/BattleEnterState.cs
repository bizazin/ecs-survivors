using Code.Gameplay;
using Code.Gameplay.Features.Hero.Factory;
using Code.Gameplay.Levels;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
    public class BattleEnterState : IState
    {
        private readonly GameContext _gameContext;
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISystemFactory _systems;
        private BattleFeature _battleFeature;

        public BattleEnterState(
            IGameStateMachine stateMachine,
            ILevelDataProvider levelDataProvider,
            IHeroFactory heroFactory)
        {
            _stateMachine = stateMachine;
            _levelDataProvider = levelDataProvider;
            _heroFactory = heroFactory;
        }

        public void Enter()
        {
            PlaceHero();

            _stateMachine.Enter<BattleLoopState>();
        }

        public void Exit()
        {
        }

        private void PlaceHero()
        {
            _heroFactory.CreateHero(_levelDataProvider.StartPoint);
        }
    }
}