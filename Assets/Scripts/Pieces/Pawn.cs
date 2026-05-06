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

        int[] diagonalMoves = new[] { -1, 1 };
        foreach (var xDiagPos in diagonalMoves)
        {
            Vector2Int targetPos = new Vector2Int(gridPosition.x + xDiagPos, gridPosition.y + direction);
            if (CanCapture(targetPos))
            {
                moves.Add(targetPos);
                Debug.Log("Diag: " + targetPos);
            }
        }

        return moves;
    }

    public bool IsBottomSide(bool isBottom)
    {
        return isBottomSide = isBottom;
    }

    public bool IsOnStartingRank()
    {
        return (isBottomSide && gridPosition.y == 1) || (!isBottomSide && gridPosition.y == 6);
    }

    public bool IsTileEmpty(Vector2Int pos)
    {
        Tile t = Board.Instance.GetTileAtPosition(pos.x, pos.y);
        return (t != null && !t.isOccupied);
    }

    public bool CanCapture(Vector2Int pos)
    {
        Tile t = Board.Instance.GetTileAtPosition(pos.x, pos.y);
        if (t != null)
        {
            return (t.isOccupied && t.GetChessPiece().GetTeam != this.GetTeam);
        }

        return false;
    }
}