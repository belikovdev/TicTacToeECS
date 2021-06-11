using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class NextTurnSystem : IEcsRunSystem {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        readonly GameState _gameState;
        readonly EcsFilter<NextTurn> _nextTurn;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            if (!_nextTurn.IsEmpty())
            {
                _gameState.currentTurn = _gameState.currentTurn == CellState.X ? CellState.O : CellState.X;
                Debug.Log($"Next turn: {_gameState.currentTurn}");
            }
        }
    }
}