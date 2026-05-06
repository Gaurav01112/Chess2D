using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private ChessPiece selectedPiece;

    private Tile selectedTile;

    private Color selectedColor = Color.coral;

    private bool isTileSelected = false;

    private void Awake()
    {
        Instance = this;
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

                if (selectedPiece.GetAvailableMoves().Contains(targetPos))
                {
                    if (clickedTile.isOccupied)
                    {
                        Destroy(clickedTile.GetChessPiece().gameObject);
                    }

                    selectedTile.SetPieceOnTile(null);
                    selectedPiece.MoveTo(targetPos);

                    selectedPiece.transform.SetParent(clickedTile.transform);
                    clickedTile.SetPieceOnTile(selectedPiece);
                    GameManager.Instance.ChangeTurn();
                    DeselectEverything();
                    return;
                }
                else
                {
                    //Animate the clicked tile here for showing where it got deselected
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
                            HighlightTile(selectedTile);
                            isTileSelected = true;
                            PositionManager.Instance.SetPieceValidPosition(selectedPiece);
                        }
                        else
                        {
                            ResetTile(selectedTile);
                            
                            //lastTile = selectedTile;
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

    private void HighlightTile(Tile newTile)
    {
        newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
    }

    private void ResetTile(Tile newTile)
    {
        newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
            newTile.transform.GetComponent<TileVisuals>().GetStartColor();
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