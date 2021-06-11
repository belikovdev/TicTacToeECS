using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class CellViewSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        private readonly EcsFilter<Cell, Position>.Exclude<CellViewRef> _cells = null;
        private readonly Configuration _configuration = null;
        
        void IEcsRunSystem.Run () {
            // add your run code here.

            // display cell or empty cell
            foreach (var index in _cells)
            {
                ref var pos = ref _cells.Get2(index);
                var cellView = Object.Instantiate(_configuration.cellView);

                cellView.transform.position = new Vector3(pos.Value.x + _configuration.offset * pos.Value.x, pos.Value.y + _configuration.offset * pos.Value.y);

                _cells.GetEntity(index).Get<CellViewRef>().Value = cellView;
            }
        }
    }
}