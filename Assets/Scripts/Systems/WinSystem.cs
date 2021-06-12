using Leopotam.Ecs;

namespace BelikovXO {
    sealed class WinSystem : IEcsRunSystem {
        //readonly EcsWorld _world = null;
        readonly EcsFilter<Winner, Taken> _winners;
        readonly SceneData _sceneData;
        
        void IEcsRunSystem.Run () {
            foreach (var index in _winners)
            {
                ref var winnerState = ref _winners.Get2(index).value;

                _sceneData.UI.winScreen.Show(true);
                _sceneData.UI.winScreen.SetWinner(winnerState);
            }
        }
    }
}