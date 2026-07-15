using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public bool hasMoved = false; 
    private Vector3 startPosition;
    private bool castleTriggered = false; 

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // 1. Automatic check: If the piece left home, it moved!
        if (!hasMoved && transform.position != startPosition)
        {
            hasMoved = true;
        }

        // 2. FIXED ROOK MOVEMENT DURING CASTLING
        if (!castleTriggered && hasMoved)
        {
            // Accessing the King's own gridPosition is fine because it belongs to this class instance
            if (gridPosition.x == 6)
            {
                castleTriggered = true;
                Tile rookTile = Board.Instance.GetTileAtPosition(7, gridPosition.y);
                if (rookTile != null && rookTile.isOccupied)
                {
                    ChessPiece rook = rookTile.GetChessPiece();
                    
                    // FIXED: Instead of setting gridPosition directly, call the movement method 
                    // from your base class or update the transform directly.
                    Vector2Int newRookPos = new Vector2Int(5, gridPosition.y);
                    
                    // Try calling your project's move function (e.g., rook.MoveTo(newRookPos))
                    // If you don't have one, we just snap the transform:
                    Tile targetTile = Board.Instance.GetTileAtPosition(5, gridPosition.y);
                    if (targetTile != null)
                    {
                        rook.transform.position = targetTile.transform.position;
                        // If your game requires updating the tile data manually:
                        // targetTile.OccupyTile(rook);
                        // rookTile.ClearTile();
                    }
                }
            }
            else if (gridPosition.x == 2)
            {
                castleTriggered = true;
                Tile rookTile = Board.Instance.GetTileAtPosition(0, gridPosition.y);
                if (rookTile != null && rookTile.isOccupied)
                {
                    ChessPiece rook = rookTile.GetChessPiece();
                    
                    Vector2Int newRookPos = new Vector2Int(3, gridPosition.y);
                    
                    Tile targetTile = Board.Instance.GetTileAtPosition(3, gridPosition.y);
                    if (targetTile != null)
                    {
                        rook.transform.position = targetTile.transform.position;
                    }
                }
            }
        }
    }

    public override List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        
        // --- STANDARD MOVES ---
        Vector2Int[] directions = new[] {
            new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1),
            new Vector2Int(-1, 1), new Vector2Int(1, 1), new Vector2Int(-1, -1), new Vector2Int(1, -1)
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int targetPos = new Vector2Int(gridPosition.x + dir.x, gridPosition.y + dir.y);
            Tile t = Board.Instance.GetTileAtPosition(targetPos.x, targetPos.y);
            if (t == null) continue;

            if (!t.isOccupied || t.GetChessPiece().GetTeam != this.GetTeam)
            {
                moves.Add(targetPos);
            }
        }

        // --- CASTLING VALIDATION ---
        if (hasMoved == false)
        {
            // Right Castle
            Tile f = Board.Instance.GetTileAtPosition(5, gridPosition.y); 
            Tile g = Board.Instance.GetTileAtPosition(6, gridPosition.y);
            Tile rightRookTile = Board.Instance.GetTileAtPosition(7, gridPosition.y);

            if (f != null && !f.isOccupied && g != null && !g.isOccupied && rightRookTile != null && rightRookTile.isOccupied)
            {
                Rook rook = rightRookTile.GetChessPiece() as Rook;
                if (rook != null && rook.GetTeam == this.GetTeam && rook.hasMoved == false)
                {
                    moves.Add(new Vector2Int(6, gridPosition.y)); 
                }
            }

            // Left Castle
            Tile d = Board.Instance.GetTileAtPosition(3, gridPosition.y);
            Tile c = Board.Instance.GetTileAtPosition(2, gridPosition.y);
            Tile b = Board.Instance.GetTileAtPosition(1, gridPosition.y);
            Tile leftRookTile = Board.Instance.GetTileAtPosition(0, gridPosition.y);

            if (d != null && !d.isOccupied && c != null && !c.isOccupied && b != null && !b.isOccupied && leftRookTile != null && leftRookTile.isOccupied)
            {
                Rook rook = leftRookTile.GetChessPiece() as Rook;
                if (rook != null && rook.GetTeam == this.GetTeam && rook.hasMoved == false)
                {
                    moves.Add(new Vector2Int(2, gridPosition.y));
                }
            }
        }

        return moves;
    }
}