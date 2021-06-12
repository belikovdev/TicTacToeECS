using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class InitializeBackgroundSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly SceneData _sceneData;
        readonly Configuration _configuration;
        readonly EcsFilter<InitBackgroundEvent> _initBackground;

        public void Run () {
            if (!_initBackground.IsEmpty())
            {
                var bgView = _world.NewEntity().Get<BackgroundViewRef>();
                var sz = _sceneData.camera.orthographicSize * 2;
                bgView.value = Object.Instantiate(_configuration.background);
                bgView.value.transform.localScale = new Vector3(sz, sz);
                var camPos = _sceneData.camera.transform.position;
                bgView.value.transform.position = new Vector3(camPos.x, camPos.y, 10);

                // destroy unused entities
                foreach (var index in _initBackground)
                {
                    _initBackground.GetEntity(index).Destroy();
                }
            }
        }
    }
}