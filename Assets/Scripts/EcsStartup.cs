using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        public Configuration configuration;
        public SceneData sceneData;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add(new InitializeFieldSystem())
                .Add(new CellViewSystem())
                .Add(new CameraFocusOnFieldSystem())

                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject(configuration)
                .Inject(sceneData)
                // .Inject (new NavMeshSupport ())
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}