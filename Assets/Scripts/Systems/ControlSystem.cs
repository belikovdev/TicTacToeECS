using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class ControlSystem : IEcsRunSystem {
        private readonly SceneData _sceneData;
        readonly EcsFilter<ComputerTurn> _computerTurn;
        readonly EcsFilter<Cell, Position>.Exclude<Taken> _freeCells;
        readonly EcsFilter<Winner> _winners;
        readonly EcsFilter<GameFinished> _gameFinished;

        void IEcsRunSystem.Run () {
            if (!_gameFinished.IsEmpty() || !_winners.IsEmpty())
            {
                return;
            }

            if (!_computerTurn.IsEmpty())
            {
                var count = _freeCells.GetEntitiesCount();
                var nextTurnCellIndex = Random.Range(0, count);
                var entity = _freeCells.GetEntity(nextTurnCellIndex);

                entity.Get<Clicked>();

                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var cam = _sceneData.camera;
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hitInfo))
                {
                    var cellView = hitInfo.collider.GetComponent<CellView>();
                    if (cellView)
                    {
                        cellView.entity.Get<Clicked>();
                    }
                }
            }
        }
    }
}