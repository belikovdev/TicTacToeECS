using Leopotam.Ecs;
using UnityEngine;
using System.Linq;

namespace BelikovXO
{
    sealed class CameraFocusOnFieldSystem : IEcsRunSystem
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        private readonly EcsFilter<UpdateCameraEvent> _events = null;
        private readonly SceneData _sceneData;
        private readonly Configuration _configuration;

        void IEcsRunSystem.Run()
        {
            if (!_events.IsEmpty())
            {

                // calculate the half of field size
                var size = _configuration.size / 2f + (_configuration.size - 1) * _configuration.offset / 2f;

                var cam = _sceneData.camera;

                cam.orthographicSize = size;

                cam.transform.position = new Vector3(size, size);

                _world.NewEntity().Get<InitBackgroundEvent>();
            }
        }
    }
}