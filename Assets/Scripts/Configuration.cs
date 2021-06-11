using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelikovXO
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public CellView cellView;
        public int size = 3;
        public float offset = .5f;
    }
}
