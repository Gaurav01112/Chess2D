using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Pawn : ChessPiece
{
    private bool isBottomSide = false;

    public override List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        int direction = isBottomSide ? 1 : -1;
        Vector2Int forwardOne = new Vector2Int(gridPosition.x, gridPosition.y + direction);
        if (IsTileEmpty(forwardOne))
        {
            moves.Add(forwardOne);
            if (IsOnStartingRank())
            {
                Vector2Int forwardTwo = new Vector2Int(gridPosition.x, gridPosition.y + direction * 2);
                if (IsTileEmpty(forwardTwo))
                {
                    moves.Add(forwardTwo);
                }
            }
        }

        int[] xDiagonal = new[] { -1, 1 };
        foreach (int dir in xDiagonal)
        {
            Vector2Int targetPos = new Vector2Int(gridPosition.x + dir, gridPosition.y + direction);
            if (CanCapture(targetPos))
            {
                moves.Add(targetPos);
            }
        }

        return moves;
    }

    public bool IsBottomSide(bool isBottom)
    {
        return isBottomSide = isBottom;
    }

    private bool IsTileEmpty(Vector2Int pos)
    {
        Tile t = Board.Instance.GetTileAtPosition(pos.x, pos.y);
        return (t != null && !t.isOccupied);
    }

    private bool IsOnStartingRank()
    {
        return (isBottomSide && gridPosition.y == 1) || (!isBottomSide && gridPosition.y == 6);
    }

    private bool CanCapture(Vector2Int pos)
    {
        Tile t = Board.Instance.GetTileAtPosition(pos.x, pos.y);
        if (t != null)
        {
            return t.isOccupied && t.GetChessPiece().GetTeam != this.GetTeam;
        }

        return false;
    }
}