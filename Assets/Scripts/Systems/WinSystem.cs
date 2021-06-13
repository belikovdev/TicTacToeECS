using Leopotam.Ecs;
using System.Linq;
using System.Collections;

namespace BelikovXO {
    sealed class WinSystem : IEcsRunSystem {
        //readonly EcsWorld _world = null;
        readonly EcsFilter<PlayerVsPlayer> _pvp;
        readonly EcsFilter<Winner, Taken> _winners;
        readonly SceneData _sceneData;
        
        void IEcsRunSystem.Run () {
            if (_pvp.IsEmpty())
            {
                return;
            }
            if (!_sceneData.UI.winScreen.gameObject.activeInHierarchy)
            {
                foreach (var index in _winners)
                {
                    ref var winnerState = ref _winners.Get2(index).value;

                    _sceneData.UI.winScreen.Show(true);
                    _sceneData.UI.winScreen.SetWinner(winnerState);
                    _winners.GetEntity(index).Get<GameFinished>();
                }
            }
        }
    }
}