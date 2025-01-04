using System.Collections.Generic;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.CharacterStats.Indexing;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Indexing;
using Entitas;
using Zenject;

namespace Code.Common.EntityIndices
{
    public class GameEntityIndices : IInitializable
    {
        public const string StatusesOfType = "StatusesOfType";
        public const string StatChanges = "StatChanges";
        private readonly GameContext _game;

        public GameEntityIndices(GameContext game)
        {
            _game = game;
        }

        public void Initialize()
        {
            _game.AddEntityIndex(new EntityIndex<GameEntity, StatusKey>(
                StatusesOfType,
                _game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.StatusTypeId,
                    GameMatcher.TargetId,
                    GameMatcher.Status,
                    GameMatcher.Duration,
                    GameMatcher.TimeLeft)),
                GetTargetStatusKey,
                new StatusKeyEqualityComparer()));

            _game.AddEntityIndex(new EntityIndex<GameEntity, StatKey>(
                StatChanges,
                _game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.StatChange,
                    GameMatcher.TargetId)),
                GetTargetStatKey,
                new StatKeyEqualityComparer()));
        }

        private StatKey GetTargetStatKey(GameEntity entity, IComponent component)
        {
            return new StatKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatChange)?.Value ?? entity.StatChange);
        }

        private StatusKey GetTargetStatusKey(GameEntity entity, IComponent component)
        {
            return new StatusKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatusTypeIdComponent)?.Value ?? entity.StatusTypeId);
        }
    }

    public static class ContextIndicesExtensions
    {
        public static HashSet<GameEntity> TargetStatusesOfType(this GameContext context, StatusTypeId statusTypeId,
            int targetId)
        {
            return ((EntityIndex<GameEntity, StatusKey>)context.GetEntityIndex(GameEntityIndices.StatusesOfType))
                .GetEntities(new StatusKey(targetId, statusTypeId));
        }

        public static HashSet<GameEntity> TargetStatChanges(this GameContext context, Stats stat, int targetId)
        {
            return ((EntityIndex<GameEntity, StatKey>)context.GetEntityIndex(GameEntityIndices.StatChanges))
                .GetEntities(new StatKey(targetId, stat));
        }
    }
}