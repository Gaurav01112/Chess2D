using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector2Int tilePosition;
    [SerializeField] private ChessPiece pieceOnTile;
    
    public bool isOccupied => pieceOnTile != null;

    public Vector2Int GetTilePosition()
    {
        return tilePosition;
    }
    public void SetTilePosition(Vector2Int position)
    {
        tilePosition = position;
    }

    public ChessPiece GetChessPiece()
    {
        return pieceOnTile;
    }

    public ChessPiece SetPieceOnTile(ChessPiece piece)
    {
        return pieceOnTile = piece;
    }
}