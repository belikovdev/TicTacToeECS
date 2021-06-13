using Leopotam.Ecs;

namespace BelikovXO {
    sealed class DrawSystem : IEcsRunSystem {
        readonly EcsWorld _world;
        readonly EcsFilter<PlayerVsPlayer> _pvp;
        readonly EcsFilter<Cell>.Exclude<Taken> _freeCells;
        readonly EcsFilter<Winner> _winners;
        readonly SceneData _sceneData;

        void IEcsRunSystem.Run ()
        {
            if (_pvp.IsEmpty())
            {
                return;
            }

            if (_freeCells.IsEmpty() && _winners.IsEmpty())
            {
                _sceneData.UI.winScreen.Show(true);
                _sceneData.UI.winScreen.SetWinner(CellState.Empty);

                _world.NewEntity().Get<GameFinished>();
            }
        }
    }
}