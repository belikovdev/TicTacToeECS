using Leopotam.Ecs;

namespace BelikovXO {
    sealed class AnalyzeClickSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly EcsFilter<Cell, Clicked>.Exclude<Taken> _clickedCells;
        readonly GameState _gameState;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach (var index in _clickedCells)
            {
                var entity = _clickedCells.GetEntity(index);
                entity.Get<Taken>().value = _gameState.currentTurn;
                entity.Get<NextTurn>();
            }
        }
    }
}