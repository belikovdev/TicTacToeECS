using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class CellViewSystem : IEcsRunSystem {
        private readonly EcsFilter<Cell, Position>.Exclude<CellViewRef> _cells = null;
        private readonly Configuration _configuration = null;
        
        void IEcsRunSystem.Run () {
            // add your run code here.

            // display cell or empty cell
            foreach (var index in _cells)
            {
                ref var pos = ref _cells.Get2(index);
                var cellView = Object.Instantiate(_configuration.cellView);

                cellView.transform.position = new Vector3(pos.value.x + _configuration.offset * pos.value.x, pos.value.y + _configuration.offset * pos.value.y);

                cellView.entity = _cells.GetEntity(index);

                _cells.GetEntity(index).Get<CellViewRef>().value = cellView;
            }
        }
    }
}