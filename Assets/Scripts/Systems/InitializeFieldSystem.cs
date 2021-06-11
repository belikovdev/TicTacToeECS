using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class InitializeFieldSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly Configuration _configuration;
        readonly GameState _gameState;
        
        public void Init () {
            // add your initialize code here.
            ref var board = ref _world.NewEntity().Get<Field>();

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
        }
    }
}