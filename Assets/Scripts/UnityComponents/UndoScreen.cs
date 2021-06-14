using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leopotam.Ecs;

namespace BelikovXO
{
    public class UndoScreen : Screen
    {
        public EcsWorld world;

        public void OnUndoClick()
        {
            world.NewEntity().Get<UndoEvent>();
        }
    }
}
