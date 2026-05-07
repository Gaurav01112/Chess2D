using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        Vector2Int[] directions =
        {
            new Vector2Int(0, 1), //Up
            new Vector2Int(0, -1), //Down
            new Vector2Int(1, 0), //Left
            new Vector2Int(-1, 0), //Right
        };
        foreach (Vector2Int dir in directions)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int nextPos = new Vector2Int(gridPosition.x + dir.x * i, gridPosition.y + dir.y * i);
                Tile t = Board.Instance.GetTileAtPosition(nextPos.x, nextPos.y);
                if (t == null) break;

                if (!t.isOccupied)
                {
                    moves.Add(nextPos);
                }
                else
                {
                    if (t.GetChessPiece().GetTeam != this.GetTeam)
                    {
                        moves.Add(nextPos);
                    }
                    break;
                }
            }
        }

        return moves;
    }
}