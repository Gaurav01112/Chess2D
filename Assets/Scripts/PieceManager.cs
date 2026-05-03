using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private GameObject chessPiecePrefab;

    private Team playerTeam;
    private Team opponentTeam;

    private PieceVisualManager pieceVisualManager;

    [SerializeField] private List<ChessPiece> player1Team = new List<ChessPiece>();
    [SerializeField] private List<ChessPiece> player2Team = new List<ChessPiece>();

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

    private void SpawnChessPiece(int x, int y, Sprite sprite, ChessPiece.ChessPieceType type, List<ChessPiece> team)
    {
        Tile pieceParent = Board.Instance.GetTileAtPosition(x, y);
        GameObject G = Instantiate(chessPiecePrefab.gameObject, pieceParent.transform.position, Quaternion.identity,
            pieceParent.transform);

        ChessPiece pieceObj;


        switch (type)
        {
            case ChessPiece.ChessPieceType.Pawn:
                pieceObj = G.AddComponent<Pawn>();
                break;
            case ChessPiece.ChessPieceType.King:
                pieceObj = G.AddComponent<King>();
                break;
            case ChessPiece.ChessPieceType.Queen:
                pieceObj = G.AddComponent<Queen>();
                break;
            case ChessPiece.ChessPieceType.Bishop:
                pieceObj = G.AddComponent<Bishop>();
                break;
            case ChessPiece.ChessPieceType.Knight:
                pieceObj = G.AddComponent<Knight>();
                break;
            case ChessPiece.ChessPieceType.Rook:
                pieceObj = G.AddComponent<Rook>();
                break;
            default:
                pieceObj = G.AddComponent<Pawn>();
                break;
        }

        G.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        pieceParent.SetPieceOnTile(pieceObj);
        pieceObj.SetTilePosition(new Vector2Int(x, y));
        pieceObj.SetChessPieceType(type);
        G.name = type.ToString();
        team.Add(pieceObj);
    }

    private void SpawnBottom()
    {
        SpawnChessPiece(4, 0, pieceVisualManager.GetKingSprite, ChessPiece.ChessPieceType.King, player1Team);
        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.King, player1Team);

        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.Queen, player1Team);
        SpawnChessPiece(3, 0, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.Queen, player1Team);

        SpawnChessPiece(0, 0, pieceVisualManager.GetRookSprite, ChessPiece.ChessPieceType.Rook, player1Team);
        SpawnChessPiece(7, 0, pieceVisualManager.GetRookSprite, ChessPiece.ChessPieceType.Rook, player1Team);

        SpawnChessPiece(2, 0, pieceVisualManager.GetBishopSprite, ChessPiece.ChessPieceType.Bishop, player1Team);
        SpawnChessPiece(5, 0, pieceVisualManager.GetBishopSprite, ChessPiece.ChessPieceType.Bishop, player1Team);

        SpawnChessPiece(1, 0, pieceVisualManager.GetLeftKnightSprite, ChessPiece.ChessPieceType.Knight, player1Team);
        SpawnChessPiece(6, 0, pieceVisualManager.GetRightKnightSprite, ChessPiece.ChessPieceType.Knight, player1Team);

        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(i, 1, pieceVisualManager.GetPawnSprite, ChessPiece.ChessPieceType.Pawn, player1Team);
        }

        foreach (var index in player1Team)
        {
            index.SetTeam(playerTeam);
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

        SpawnChessPiece(4, 7, pieceVisualManager.GetKingSprite, ChessPiece.ChessPieceType.King, player2Team);
        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.King, player2Team);

        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.Queen, player2Team);
        SpawnChessPiece(3, 7, pieceVisualManager.GetQueenSprite, ChessPiece.ChessPieceType.Queen, player2Team);

        SpawnChessPiece(0, 7, pieceVisualManager.GetRookSprite, ChessPiece.ChessPieceType.Rook, player2Team);
        SpawnChessPiece(7, 7, pieceVisualManager.GetRookSprite, ChessPiece.ChessPieceType.Rook, player2Team);

        SpawnChessPiece(2, 7, pieceVisualManager.GetBishopSprite, ChessPiece.ChessPieceType.Bishop, player2Team);
        SpawnChessPiece(5, 7, pieceVisualManager.GetBishopSprite, ChessPiece.ChessPieceType.Bishop, player2Team);

        SpawnChessPiece(1, 7, pieceVisualManager.GetLeftKnightSprite, ChessPiece.ChessPieceType.Knight, player2Team);
        SpawnChessPiece(6, 7, pieceVisualManager.GetRightKnightSprite, ChessPiece.ChessPieceType.Knight, player2Team);

        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(i, 6, pieceVisualManager.GetPawnSprite, ChessPiece.ChessPieceType.Pawn, player2Team);
        }

        foreach (var index in player2Team)
        {
            index.SetTeam(opponentTeam);
        }
    }
}