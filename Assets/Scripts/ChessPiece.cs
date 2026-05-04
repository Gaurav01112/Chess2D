using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    [SerializeField] protected Vector2Int gridPosition;
    [SerializeField] private ChessPieceType pieceType;
    [SerializeField] private Team team;
    public enum ChessPieceType
    {
        None,
        King,
        Queen,
        Pawn,
        Knight,
        Bishop,
        Rook,
    }

    public abstract List<Vector2Int> GetAvailableMoves();
    
    public Vector2Int GetGridPosition => gridPosition;
    
    public void SetTilePosition(Vector2Int position)
    {
        gridPosition = position;
    }

    public ChessPieceType GetPieceType => pieceType;

    public ChessPieceType SetChessPieceType(ChessPieceType newType)
    {
        return pieceType = newType;
    }
    public Team GetTeam => team;
    public Team SetTeam(Team newTeam)
    {
        return team = newTeam;
    }
}