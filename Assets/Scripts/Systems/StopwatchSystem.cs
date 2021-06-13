using Leopotam.Ecs;
using UnityEngine;

namespace BelikovXO
{
    sealed class StopwatchSystem : IEcsRunSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        readonly SceneData _sceneData;
        readonly EcsFilter<Stopwatch> _stopwatch;
        readonly EcsFilter<GameFinished> _gameFinished;

        void IEcsRunSystem.Run()
        {
            if (!_gameFinished.IsEmpty()) {
                foreach (var index in _stopwatch)
                {
                    _stopwatch.GetEntity(index).Destroy();
                }
            }

            foreach (var index in _stopwatch)
            {
                ref var sw = ref _stopwatch.Get1(index);
                sw.value += Time.deltaTime;
                _sceneData.UI.stopwatchScreeen.text.text = sw.value.ToString("0.00");
            }
        }
    }
}