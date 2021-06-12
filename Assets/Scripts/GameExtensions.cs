using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace BelikovXO
{
    public static class GameExtensions
    {
        public static int GetLongestChain(this Dictionary<Vector2Int, EcsEntity> field, Vector2Int position)
        {
            var entity = field[position];

            if (!entity.Has<Taken>())
            {
                return 0;
            }

            var state = entity.Get<Taken>().value;

            var horizontalChainLength = 1;
            var left = new Vector2Int(-1, 0);
            var right = new Vector2Int(1, 0);
            horizontalChainLength += GetChainLengthInDirection(field, left, position + left, state) + GetChainLengthInDirection(field, right, position + right, state);

            var verticalChainLength = 1;
            var top = new Vector2Int(0, 1);
            var bottom = new Vector2Int(0, -1);
            verticalChainLength += GetChainLengthInDirection(field, top, position + top, state) + GetChainLengthInDirection(field, bottom, position + bottom, state);

            var diagonalLengthTopLeft = 1;
            var topLeft = new Vector2Int(-1, 1);
            var bottomRight = new Vector2Int(1, -1);
            diagonalLengthTopLeft += GetChainLengthInDirection(field, topLeft, position + topLeft, state) + GetChainLengthInDirection(field, bottomRight, position + bottomRight, state);

            var diagonalLengthTopRight = 1;
            var topRight = new Vector2Int(1, 1);
            var bottomLeft = new Vector2Int(-1, -1);
            diagonalLengthTopRight += GetChainLengthInDirection(field, topRight, position + topRight, state) + GetChainLengthInDirection(field, bottomLeft, position + bottomLeft, state);


            return Mathf.Max(horizontalChainLength, verticalChainLength, diagonalLengthTopLeft, diagonalLengthTopRight);
        }

        private static int GetChainLengthInDirection(Dictionary<Vector2Int, EcsEntity> field, Vector2Int direction, Vector2Int startPos, CellState state)
        {
            var result = 0;
            var currPos = startPos;

            while (field.TryGetValue(currPos, out var currEntity))
            {
                if (!currEntity.Has<Taken>())
                {
                    break;
                }

                var currState = currEntity.Get<Taken>().value;

                if (currState != state)
                {
                    break;
                }

                result++;
                currPos += direction;
            }

            return result;
        }
    }
}