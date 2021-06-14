using Leopotam.Ecs;
using UnityEngine;
using System;

namespace BelikovXO {
    sealed class AnalyzeTurnSystem: IEcsRunSystem {
        readonly EcsFilter<Cell, Clicked, Position>.Exclude<Taken> _clickedCells;
        readonly GameState _gameState;
        
        void IEcsRunSystem.Run () {

            foreach (var index in _clickedCells)
            {
                ref var entity = ref _clickedCells.GetEntity(index);
                entity.Get<Taken>().value = _gameState.currentTurn;
                _gameState.turnsHistory.Add(entity.Get<Position>().value);
                entity.Get<CheckWinEvent>();
                entity.Get<NextTurn>();
            }
        }
    }
}