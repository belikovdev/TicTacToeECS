using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class NextTurnSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly GameState _gameState;
        readonly EcsFilter<NextTurn> _nextTurn;
        readonly EcsFilter<PlayerVsComputer> _pvc;
        readonly EcsFilter<ComputerTurn> _ct;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            if (!_nextTurn.IsEmpty())
            {
                if (!_pvc.IsEmpty() && _ct.IsEmpty())
                {
                    Debug.Log("Computer turn");
                    _world.NewEntity().Get<ComputerTurn>();
                }
                else if (!_ct.IsEmpty())
                {
                    foreach (var index in _ct)
                    {
                        _ct.GetEntity(index).Destroy();
                    }
                }

                _gameState.currentTurn = _gameState.currentTurn == CellState.X ? CellState.O : CellState.X;
                Debug.Log($"Next turn: {_gameState.currentTurn}");
            }
        }
    }
}