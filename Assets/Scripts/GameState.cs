using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class GameState
{
    public CellState currentTurn = CellState.X;
    public readonly Dictionary<Vector2Int, EcsEntity> field = new Dictionary<Vector2Int, EcsEntity>();
    public readonly List<Vector2Int> turnsHistory = new List<Vector2Int>();
}
