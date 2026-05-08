using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        Vector2Int[] directions = new[]
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
        };
        foreach (Vector2Int dir in directions)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int targetPos = new Vector2Int(gridPosition.x + dir.x * i, gridPosition.y + dir.y * i);
                Tile t = Board.Instance.GetTileAtPosition(targetPos.x, targetPos.y);

                if (t == null) break;

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

                    break;
                }
            }
        }

        return moves;
    }
}