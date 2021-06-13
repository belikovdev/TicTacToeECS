using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class ControlSystem : IEcsRunSystem {
        private readonly SceneData _sceneData;
        readonly EcsFilter<ComputerTurn> _computerTurn;
        
        void IEcsRunSystem.Run () {
            if (!_computerTurn.IsEmpty())
            {
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