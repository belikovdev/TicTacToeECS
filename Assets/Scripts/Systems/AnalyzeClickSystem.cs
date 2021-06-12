using Leopotam.Ecs;
using UnityEngine;
using System;

namespace BelikovXO {
    sealed class AnalyzeClickSystem : IEcsRunSystem {
        readonly EcsFilter<Cell, Clicked, Position>.Exclude<Taken> _clickedCells;
        readonly GameState _gameState;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach (var index in _clickedCells)
            {
                var entity = _clickedCells.GetEntity(index);
                entity.Get<Taken>().value = _gameState.currentTurn;
                _gameState.turnsHistory.Add(entity.Get<Position>().value);
                entity.Get<CheckWinEvent>();
                entity.Get<NextTurn>();
            }
        }
    }
}