using System.Linq;
using Code.Common.EntityIndices;
using Code.Common.Extensions;
using Code.Gameplay.Features.Statuses.Factory;

namespace Code.Gameplay.Features.Statuses.Applier
{
    public class StatusApplier : IStatusApplier
    {
        private readonly GameContext _game;
        private readonly IStatusFactory _statusFactory;

        public StatusApplier(IStatusFactory statusFactory, GameContext game)
        {
            _statusFactory = statusFactory;
            _game = game;
        }

        public GameEntity ApplyStatus(StatusSetup setup, int producerId, int targetId)
        {
            var status = _game.TargetStatusesOfType(setup.StatusTypeId, targetId).FirstOrDefault();
            if (status != null)
                return status.ReplaceTimeLeft(setup.Duration);
            return _statusFactory.CreateStatus(setup, producerId, targetId)
                .With(x => x.isApplied = true);
        }
    }
}