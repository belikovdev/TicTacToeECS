using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class InitializeFieldSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly Configuration _configuration;
        readonly GameState _gameState;
        readonly SceneData _sceneData;
        readonly EcsFilter<PlayerVsPlayer> _pvp;
        readonly EcsFilter<Cell> _cells;
        
        public void Run () {
            if (_pvp.IsEmpty())
            {
                return;
            }

            if (!_cells.IsEmpty())
            {
                return;
            }

            for (int x = 0; x < _configuration.size; x++)
            {
                for (int y = 0; y < _configuration.size; y++)
                {
                    var cellEntity = _world.NewEntity();
                    cellEntity.Get<Cell>();
                    var fieldPos = new Vector2Int(x, y);
                    cellEntity.Get<Position>().value = fieldPos;
                    _gameState.field[fieldPos] = cellEntity;
                }
            }

            _world.NewEntity().Get<UpdateCameraEvent>();
            _world.NewEntity().Get<Stopwatch>();
            _sceneData.UI.stopwatchScreeen.Show(true);
            _sceneData.UI.mainMenuScreen.Show(false);
        }
    }
}