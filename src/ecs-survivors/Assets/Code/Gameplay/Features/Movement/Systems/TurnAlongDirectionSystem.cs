using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class TurnAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public TurnAlongDirectionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.SpriteRenderer,
                GameMatcher.TurnedAlongDirection,
                GameMatcher.Direction));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                float scale = Mathf.Abs(mover.SpriteRenderer.transform.localScale.x);
                mover.SpriteRenderer.transform.SetScaleX(scale * FaceDirection(mover));
            }
        }
        
        private int FaceDirection(GameEntity mover) => 
            mover.Direction.x <= 0 ? -1 : 1;
    }
}