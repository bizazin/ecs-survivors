using System;
using Code.Common.Entity;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.Features.Simulation;
using Code.Progress.Data;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class ActualizeProgressState: SimpleState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly ITimeService _timeService;
        private readonly ISystemFactory _systemFactory;
        private readonly ISaveLoadService _saveLoadService;
        
        private ActualizationFeature _actualizationFeature;
        private TimeSpan _twoDays = TimeSpan.FromDays(2);


        public ActualizeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider,
            ISystemFactory systemFactory,
            ISaveLoadService saveLoadService,
            ITimeService timeService)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _timeService = timeService;
            _systemFactory = systemFactory;
            _saveLoadService = saveLoadService;
        }

        public override void Enter()
        {
            _actualizationFeature = _systemFactory.Create<ActualizationFeature>();

            //simulation
            // _progressProvider.ProgressData.LastSimulationTickTime = _timeService.UtcNow - _twoDays;
            
            ActualizeProgress(_progressProvider.ProgressData);  
            
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        private void ActualizeProgress(ProgressData data)
        {
            // test booster
            /*CreateMetaEntity.Empty()
                .AddGoldGainBoost(1)
                .AddDuration((float)TimeSpan.FromDays(1).TotalSeconds);*/
            
            _actualizationFeature.Initialize();
            _actualizationFeature.DeactivateReactiveSystems();

            DateTime until = GetLimiedTime(data);

            Debug.Log($"Actualizing {(until - data.LastSimulationTickTime).TotalSeconds} seconds");
            
            while (data.LastSimulationTickTime < until)
            {
                var tick = CreateMetaEntity
                    .Empty()
                    .AddTick(MetaConstants.SimulationTickSeconds);
                
                _actualizationFeature.Execute();
                _actualizationFeature.Cleanup();
                
                tick.Destroy();
            }

            data.LastSimulationTickTime = _timeService.UtcNow;
            
            _saveLoadService.SaveProgress();
        }

        private DateTime GetLimiedTime(ProgressData data)
        {
            return _timeService.UtcNow - data.LastSimulationTickTime < _twoDays
                ? _timeService.UtcNow
                : data.LastSimulationTickTime + _twoDays;
        }

        public void BeginExit()
        {
            _actualizationFeature.Cleanup();
            _actualizationFeature.TearDown();
            _actualizationFeature = null;
        }
    }
}