using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO
{
    sealed class UndoSystem : IEcsRunSystem
    {
        readonly GameState _gameState;
        readonly EcsFilter<UndoEvent> _undoEvent;

        void IEcsRunSystem.Run()
        {
            var his = _gameState.turnsHistory;

            if (his.Count == 0)
            {
                return;
            }

            if (_undoEvent.IsEmpty())
            {
                return;
            }

            // this approach will allow easily add timeout before computer's turn

            // remove last turn
            var entity = _gameState.field[his[his.Count - 1]];
            entity.Del<Taken>();
            Object.Destroy(entity.Get<TakenViewRef>().value.gameObject);
            entity.Del<TakenViewRef>();
            his.RemoveAt(his.Count - 1);

            // remove one turn before last
            if (his.Count - 1 >= 0)
            {
                entity = _gameState.field[his[his.Count - 1]];
                entity.Del<Taken>();
                Object.Destroy(entity.Get<TakenViewRef>().value.gameObject);
                entity.Del<TakenViewRef>();
                his.RemoveAt(his.Count - 1);
            }
        }
    }
}