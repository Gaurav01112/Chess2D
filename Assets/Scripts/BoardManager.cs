using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public static event EventHandler OnPieceCapture;
    public static event EventHandler OnPieceMove;

    public static BoardManager Instance;

    private ChessPiece selectedPiece;

    private Tile selectedTile;

    private Tile firstStartTile;
    private Tile firstEndTile;

    private Color selectedColor = Color.lightGreen;

    private bool isTileSelected = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        OnPieceMove = null;
        OnPieceCapture = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            int x = Mathf.RoundToInt(mousePos.x);
            int y = Mathf.RoundToInt(mousePos.y);

            Tile clickedTile = Board.Instance.GetTileAtPosition(x, y);

            if (clickedTile == null)
            {
                DeselectEverything();
                return;
            }

            if (selectedPiece != null && (!clickedTile.isOccupied ||
                                          clickedTile.GetChessPiece().GetTeam != GameManager.Instance.GetTeam))
            {
                Vector2Int targetPos = new Vector2Int(x, y);

                if (GetLegalMoves(selectedPiece).Contains(targetPos))
                {
                    // 1. Reset the previous turn's highlights back to normal board colors
                    if (firstStartTile != null)
                    {
                        Tile oldStart = firstStartTile;
                        firstStartTile = null;
                        ResetTile(oldStart);
                    }

                    if (firstEndTile != null)
                    {
                        Tile oldEnd = firstEndTile;
                        firstEndTile = null;
                        ResetTile(oldEnd);
                    }

                    // 2. Execute Captures vs. Normal Moves
                    if (clickedTile.isOccupied)
                    {
                        ChessPiece pieceToCapture = clickedTile.GetChessPiece();
                        PieceManager.Instance.GetAllPiecesList().Remove(pieceToCapture);
                        Destroy(pieceToCapture.gameObject);
                        OnPieceCapture?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        OnPieceMove?.Invoke(this, EventArgs.Empty);
                    }

                    // --- CRITICAL FIX: Save the new move coordinates ---
                    firstStartTile = selectedTile; // Stores where we came from
                    firstEndTile = clickedTile;   // Stores where we landed

                    // Print coordinates to the console
                    TileVisuals click = clickedTile.GetComponent<TileVisuals>();

                    // 3. Move the piece physically & logically
                    selectedTile.SetPieceOnTile(null);
                    selectedPiece.MoveTo(targetPos);

                    selectedPiece.transform.SetParent(clickedTile.transform);
                    clickedTile.SetPieceOnTile(selectedPiece);
                    
                    Team nextTeam = (selectedPiece.GetTeam == Team.TeamWhite) ? Team.TeamBlack : Team.TeamWhite;
                    CheckForGameOver(nextTeam);
                    GameManager.Instance.ChangeTurn();
                    
                    // 4. Clean up selections and paint the new move highlights
                    DeselectEverything();
                    HighlightTile(firstStartTile);
                    HighlightTile(firstEndTile);
                    return;
                }
                else
                {
                    DeselectEverything();
                }
            }
            else if (clickedTile != null)
            {
                if (clickedTile.isOccupied)
                {
                    if (GameManager.Instance.GetTeam == clickedTile.GetChessPiece().GetTeam)
                    {
                        if (!isTileSelected)
                        {
                            selectedTile = clickedTile;
                            selectedPiece = selectedTile.GetChessPiece();
                            PositionManager.Instance.SetPieceValidPosition(selectedPiece);
                            isTileSelected = true;


                            HighlightTile(selectedTile);

                        }
                        else
                        {
                            ResetTile(selectedTile);

                            selectedTile = clickedTile;
                            HighlightTile(selectedTile);
                            selectedPiece = selectedTile.GetChessPiece();
                            PositionManager.Instance.SetPieceValidPosition(selectedPiece);
                            clickedTile = null;
                        }
                    }
                    else
                    {
                        DeselectEverything();
                    }
                }
            }
        }
    }

    public void CheckForGameOver(Team teamInTurn)
    {
        if (!HasAnyLegalMoves(teamInTurn))
        {
            if (IsKingInCheck(teamInTurn))
            {
                Debug.Log("CHECKMATE");
                UIManager.Instance.gameOverUI.SetActive(true);
            }
            else
            {
                Debug.Log("STALEMATE");
                UIManager.Instance.gameOverUI.SetActive(true);
            }
        }
    }

    public bool HasAnyLegalMoves(Team team)
    {
        List<ChessPiece> teamPieces = PieceManager.Instance.GetPiecesByTeam(team);
        foreach (var piece in teamPieces)
        {
            if (GetLegalMoves(piece).Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    public List<Vector2Int> GetLegalMoves(ChessPiece piece)
    {
        List<Vector2Int> pseudoMoves = piece.GetAvailableMoves();
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        Vector2Int originalPos = piece.GetGridPosition;
        Tile originalTile = Board.Instance.GetTileAtPosition(originalPos.x, originalPos.y);
        
        foreach (var targetPos in pseudoMoves)
        {
            Tile targetTile = Board.Instance.GetTileAtPosition(targetPos.x, targetPos.y);
            ChessPiece capturedPiece = targetTile.GetChessPiece();
            if (capturedPiece != null) PieceManager.Instance.GetAllPiecesList().Remove(capturedPiece);
            targetTile.SetPieceOnTile(piece);
            piece.SetTilePosition(targetPos);
            originalTile.SetPieceOnTile(null);

            if (!IsKingInCheck(piece.GetTeam))
            {
                legalMoves.Add(targetPos);
            }

            targetTile.SetPieceOnTile(capturedPiece);
            originalTile.SetPieceOnTile(piece);
            piece.SetTilePosition(originalPos);

            if (capturedPiece != null)
            {
                capturedPiece.SetTilePosition(targetPos);
                PieceManager.Instance.GetAllPiecesList().Add(capturedPiece);
            }
        }

        return legalMoves;
    }

    private bool IsKingInCheck(Team team)
    {
        ChessPiece king = PieceManager.Instance.GetKing(team);
        if (king == null) return false;
        Vector2Int kingPos = king.GetGridPosition;
        Team opponentTeam = (team == Team.TeamWhite) ? Team.TeamBlack : Team.TeamWhite;
        List<ChessPiece> opponentPieces = PieceManager.Instance.GetPiecesByTeam(opponentTeam);
        foreach (ChessPiece enemy in opponentPieces)
        {
            if (enemy.GetAvailableMoves().Contains(kingPos))
            {
                return true;
            }
        }

        return false;
    }

    private void HighlightTile(Tile newTile)
    {
        newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
    }

    private void ResetTile(Tile tileToReset)
    {
        if (tileToReset == null) return;
        if (tileToReset == firstStartTile || tileToReset == firstEndTile)
        {
            tileToReset.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
        }
        else
        {
            tileToReset.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                tileToReset.transform.GetComponent<TileVisuals>().GetStartColor();
        }

        PositionManager.Instance.HideAllDots();
    }

    private void DeselectEverything()
    {
        if (selectedTile != null)
        {
            ResetTile(selectedTile);
        }

        selectedPiece = null;
        selectedTile = null;
        isTileSelected = false;
        PositionManager.Instance.HideAllDots();
    }
}