using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelikovXO
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public CellView cellView;
        public SignView OView;
        public SignView XView;
        public BackgroundView background;
        public int size = 3;
        public int chainSize = 3;
        public float offset = .5f;
    }
}
