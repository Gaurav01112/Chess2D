using System;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private ChessPiece chessPiecePrefab;

    private PieceVisualManager pieceVisualManager;

    private int playerPieceColorRandom;  
    private void Awake()
    {
        pieceVisualManager = GetComponent<PieceVisualManager>();
    }

    private void Start()
    {
        SpawnKing();
        SpawnQueen();
        SpawnPawn();
        SpawnRook();
        SpawnBishop();
        SpawnKnight();
    }

    private void SpawnPiece(int x, int y, ChessPiece piecePrefab, Sprite pieceSprite,
        ChessPiece.ChessPieceType pieceType)
    {
        Tile targetTile = Board.Instance.GetTileAtPosition(x, y);

        if (targetTile != null)
        {
            ChessPiece G = Instantiate(piecePrefab, targetTile.transform.position, Quaternion.identity);
            G.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pieceSprite;
            G.transform.parent = targetTile.transform;
            G.SetTilePosition(targetTile.GetTilePosition());
            G.SetChessPieceType(pieceType);
            G.name = pieceType.ToString();
            targetTile.SetPieceOnTile(G);
        }
    }
    private void SpawnKing()
    {
        SpawnPiece(4, 0, chessPiecePrefab, pieceVisualManager.GetWhiteKingSprite, ChessPiece.ChessPieceType.King);
        SpawnPiece(4, 7, chessPiecePrefab, pieceVisualManager.GetBlackKingSprite, ChessPiece.ChessPieceType.King);
    }

    private void SpawnQueen()
    {
        SpawnPiece(3, 0, chessPiecePrefab, pieceVisualManager.GetWhiteQueenSprite, ChessPiece.ChessPieceType.Queen);
        SpawnPiece(3, 7, chessPiecePrefab, pieceVisualManager.GetBlackQueenSprite, ChessPiece.ChessPieceType.Queen);
    }

    private void SpawnPawn()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(i, 1, chessPiecePrefab, pieceVisualManager.GetWhitePawnSprite, ChessPiece.ChessPieceType.Pawn);
        }

        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(i, 6, chessPiecePrefab, pieceVisualManager.GetBlackPawnSprite, ChessPiece.ChessPieceType.Pawn);
        }
    }

    private void SpawnRook()
    {
        SpawnPiece(0, 0, chessPiecePrefab, pieceVisualManager.GetWhiteRookSprite, ChessPiece.ChessPieceType.Rook);
        SpawnPiece(7, 0, chessPiecePrefab, pieceVisualManager.GetWhiteRookSprite, ChessPiece.ChessPieceType.Rook);

        SpawnPiece(0, 7, chessPiecePrefab, pieceVisualManager.GetBlackRookSprite, ChessPiece.ChessPieceType.Rook);
        SpawnPiece(7, 7, chessPiecePrefab, pieceVisualManager.GetBlackRookSprite, ChessPiece.ChessPieceType.Rook);
    }

    private void SpawnBishop()
    {
        SpawnPiece(2, 0, chessPiecePrefab, pieceVisualManager.GetWhiteBishopSprite, ChessPiece.ChessPieceType.Bishop);
        SpawnPiece(5, 0, chessPiecePrefab, pieceVisualManager.GetWhiteBishopSprite, ChessPiece.ChessPieceType.Bishop);

        SpawnPiece(2, 7, chessPiecePrefab, pieceVisualManager.GetBlackBishopSprite, ChessPiece.ChessPieceType.Bishop);
        SpawnPiece(5, 7, chessPiecePrefab, pieceVisualManager.GetBlackBishopSprite, ChessPiece.ChessPieceType.Bishop);
    }

    private void SpawnKnight()
    {
        SpawnPiece(1, 0, chessPiecePrefab, pieceVisualManager.GetWhiteLeftKnightSprite,
            ChessPiece.ChessPieceType.Knight);
        SpawnPiece(6, 0, chessPiecePrefab, pieceVisualManager.GetWhiteRightKnightSprite,
            ChessPiece.ChessPieceType.Knight);

        SpawnPiece(1, 7, chessPiecePrefab, pieceVisualManager.GetBlackLeftKnightSprite,
            ChessPiece.ChessPieceType.Knight);
        SpawnPiece(6, 7, chessPiecePrefab, pieceVisualManager.GetBlackRightKnightSprite,
            ChessPiece.ChessPieceType.Knight);
    }
}