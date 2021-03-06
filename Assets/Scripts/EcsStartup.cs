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


            var gameState = new GameState();

            _systems
                // init
                .Add(new InitializeFieldSystem())

                // run
                .Add(new CellViewSystem())
                .Add(new CameraFocusOnFieldSystem())
                .Add(new InitializeBackgroundSystem())
                .Add(new ControlSystem())
                .Add(new AnalyzeTurnSystem())
                .Add(new NextTurnSystem())
                .Add(new TakenViewSystem())
                .Add(new CheckWinSystem())
                .Add(new WinSystem())
                .Add(new DrawSystem())
                .Add(new StopwatchSystem())
                .Add(new UndoSystem())

                // register one-frame components (order is important), for example:
                .OneFrame<UpdateCameraEvent>()
                .OneFrame<InitBackgroundEvent>()
                .OneFrame<Clicked>()
                .OneFrame<NextTurn>()
                .OneFrame<UndoEvent>()
                .OneFrame<CheckWinEvent>()
                .OneFrame<GameFinished>()

                // inject service instances here (order doesn't important), for example:
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(gameState)
                // .Inject (new NavMeshSupport ())
                .Init();

            // explicit setting world can be avoided with another UnityPackage for ecs-ui but
            // I keep it here to make it more simple
            sceneData.UI.undoScreen.world = _world;
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

        public void OnPlayerVsPlayerClick()
        {
            _world.NewEntity().Get<PlayerVsPlayer>();
        }

        public void OnPlayerVsComputerClick()
        {
            _world.NewEntity().Get<PlayerVsComputer>();
            sceneData.UI.undoScreen.Show(true);
        }

        public void OnExitClick()
        {

        }
    }
}