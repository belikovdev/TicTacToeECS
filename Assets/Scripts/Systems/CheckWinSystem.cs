using Leopotam.Ecs;

namespace BelikovXO {
    sealed class CheckWinSystem : IEcsRunSystem {
        readonly Configuration _configuration;
        readonly GameState _gameState;
        readonly EcsFilter<Position, Taken, CheckWinEvent> _checkWin;
        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach (var index in _checkWin)
            {
                ref var position = ref _checkWin.Get1(index);
                var chainLength = _gameState.field.GetLongestChain(position.value);

                if (chainLength >= _configuration.chainSize)
                {
                    _checkWin.GetEntity(index).Get<Winner>();
                }
            }
        }
    }
}