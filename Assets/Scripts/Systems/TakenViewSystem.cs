using System;
using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO {
    sealed class TakenViewSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly Configuration _configuration;
        readonly EcsFilter<Taken, CellViewRef>.Exclude<TakenViewRef> _takenCells;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach (var index in _takenCells)
            {
                var position = _takenCells.Get2(index).value.transform.position;
                var takenState = _takenCells.Get1(index).value;

                SignView sign = null;

                switch (takenState)
                {
                    case CellState.X:
                        sign = _configuration.XView;
                        break;
                    case CellState.O:
                        sign = _configuration.OView;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var instance = UnityEngine.Object.Instantiate(sign, position, Quaternion.identity);
                _takenCells.GetEntity(index).Get<TakenViewRef>().value = instance;
            }
        }
    }
}