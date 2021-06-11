using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class ControlSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        private readonly SceneData _sceneData;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            if (Input.GetMouseButtonDown(0))
            {
                var cam = _sceneData.camera;
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hitInfo))
                {
                    var cellView = hitInfo.collider.GetComponent<CellView>();
                    if (cellView)
                    {
                        Debug.Log("Clicked");
                        cellView.entity.Get<Clicked>();
                    }
                }
            }
        }
    }
}