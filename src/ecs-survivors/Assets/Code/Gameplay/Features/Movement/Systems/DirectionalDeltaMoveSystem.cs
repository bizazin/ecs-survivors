using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _movers;

        public DirectionalDeltaMoveSystem(GameContext game, ITimeService time)
        {
            _time = time;

            _movers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldPosition,
                GameMatcher.Speed,
                GameMatcher.Direction,
                GameMatcher.Moving,
                GameMatcher.MovementAvailable));
        }
        
        public void Execute()
        {
            foreach (GameEntity mover in _movers) 
                mover.ReplaceWorldPosition((Vector2)mover.WorldPosition + mover.Speed * mover.Direction * _time.DeltaTime );
        }
    }
}