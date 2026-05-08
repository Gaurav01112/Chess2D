using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        Vector2Int[] direction = new[]
        {
            new Vector2Int(-1, 2),
            new Vector2Int(1, 2),
            new Vector2Int(-2, 1),
            new Vector2Int(2, 1),
            new Vector2Int(-2, -1),
            new Vector2Int(2, -1),
            new Vector2Int(-1, -2),
            new Vector2Int(1, -2),
        };
        foreach (Vector2Int dir in direction)
        {
            Vector2Int targetPos = new Vector2Int(gridPosition.x + dir.x, gridPosition.y + dir.y);
            Tile t = Board.Instance.GetTileAtPosition(targetPos.x, targetPos.y);

            if (t == null) continue;

            if (!t.isOccupied)
            {
                moves.Add(targetPos);
            }
            else
            {
                if (t.GetChessPiece().GetTeam != this.GetTeam)
                {
                    moves.Add(targetPos);
                }
            }
        }
        return moves;
    }
}