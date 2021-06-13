using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class ComputerLogicSystem : IEcsRunSystem {
        readonly GameState _gameState;
        readonly EcsFilter<ComputerTurn> _computerTurn;
        readonly EcsFilter<Cell, Position>.Exclude<Taken> _freeCells;
        
        void IEcsRunSystem.Run () {
            if (_computerTurn.IsEmpty())
            {
                return;
            }

            var count = _freeCells.GetEntitiesCount();
            var nextTurnCellIndex = Random.Range(0, count - 1);
            Debug.Log($"Entities count: {count}, next index: {nextTurnCellIndex}");
            var entity = _freeCells.GetEntity(nextTurnCellIndex);
            entity.Get<Taken>().value = _gameState.currentTurn;
            _gameState.turnsHistory.Add(entity.Get<Position>().value);
            entity.Get<CheckWinEvent>();
            entity.Get<NextTurn>();
        }
    }
}