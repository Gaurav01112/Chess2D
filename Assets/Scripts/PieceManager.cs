using System;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private ChessPiece chessPiecePrefab;

    private Team playerTeam;
    private Team opponentTeam;

    private PieceVisualManager pieceVisualManager;

    private void Awake()
    {
        pieceVisualManager = GetComponent<PieceVisualManager>();
    }

    private void Start()
    {
        playerTeam = (UnityEngine.Random.Range(0, 2) == 0) ? Team.TeamWhite : Team.TeamBlack;
        opponentTeam = (playerTeam == Team.TeamWhite) ? Team.TeamBlack : Team.TeamWhite;
        GameManager.Instance.SetIsWhite(playerTeam == Team.TeamWhite);

        SpawnBottom();
        SpawnTop();
    }

    private void SpawnChessPiece(int x, int y, Sprite sprite, ChessPiece.ChessPieceType type)
    {
        Tile pieceParent = Board.Instance.GetTileAtPosition(x, y);
        ChessPiece G = Instantiate(chessPiecePrefab, pieceParent.transform.position, Quaternion.identity,
            pieceParent.transform);
        G.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;

        pieceParent.SetPieceOnTile(G);
        G.SetChessPieceType(type);
        G.name = type.ToString();
    }

    private void SpawnBottom()
    {
        SpawnChessPiece(4, 0, pieceVisualManager.GetKingSprite,ChessPiece.ChessPieceType.King);
        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.King);

        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.Queen);
        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.Queen);

        SpawnChessPiece(0, 0, pieceVisualManager.GetRookSprite,ChessPiece.ChessPieceType.Rook);
        SpawnChessPiece(7, 0, pieceVisualManager.GetRookSprite,ChessPiece.ChessPieceType.Rook);

        SpawnChessPiece(2, 0, pieceVisualManager.GetBishopSprite,ChessPiece.ChessPieceType.Bishop);
        SpawnChessPiece(5, 0, pieceVisualManager.GetBishopSprite,ChessPiece.ChessPieceType.Bishop);

        SpawnChessPiece(1, 0, pieceVisualManager.GetLeftKnightSprite,ChessPiece.ChessPieceType.Knight);
        SpawnChessPiece(6, 0, pieceVisualManager.GetRightKnightSprite,ChessPiece.ChessPieceType.Knight);

        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(i, 1, pieceVisualManager.GetPawnSprite,ChessPiece.ChessPieceType.Pawn);
        }
    }

    private void SpawnTop()
    {
        if (GameManager.Instance.IsWhite)
        {
            GameManager.Instance.SetIsWhite(false);
        }
        else
        {
            GameManager.Instance.SetIsWhite(true);
        }

        SpawnChessPiece(4, 7, pieceVisualManager.GetKingSprite,ChessPiece.ChessPieceType.King);
        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.King);

        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.Queen);
        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite,ChessPiece.ChessPieceType.Queen);

        SpawnChessPiece(0, 7, pieceVisualManager.GetRookSprite,ChessPiece.ChessPieceType.Rook);
        SpawnChessPiece(7, 7, pieceVisualManager.GetRookSprite,ChessPiece.ChessPieceType.Rook);

        SpawnChessPiece(2, 7, pieceVisualManager.GetBishopSprite,ChessPiece.ChessPieceType.Bishop);
        SpawnChessPiece(5, 7, pieceVisualManager.GetBishopSprite,ChessPiece.ChessPieceType.Bishop);

        SpawnChessPiece(1, 7, pieceVisualManager.GetLeftKnightSprite,ChessPiece.ChessPieceType.Knight);
        SpawnChessPiece(6, 7, pieceVisualManager.GetRightKnightSprite,ChessPiece.ChessPieceType.Knight);

        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(i, 6, pieceVisualManager.GetPawnSprite,ChessPiece.ChessPieceType.Pawn);
        }
    }
}