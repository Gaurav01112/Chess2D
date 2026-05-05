using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private ChessPiece selectedPiece;

    private Tile selectedTile;
    private Tile lastTile;

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

            if (clickedTile != null && !clickedTile.isOccupied && selectedPiece != null)
            {
                Vector2Int targetPos = new Vector2Int(x, y);

                if (selectedPiece.GetAvailableMoves().Contains(targetPos))
                {
                    selectedPiece.MoveTo(targetPos);
                    selectedPiece.transform.SetParent(selectedTile.transform);
                    selectedTile.SetPieceOnTile(selectedPiece);
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

            if (clickedTile != null)
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
                            lastTile = selectedTile;
                            selectedTile = clickedTile;
                            ResetTile(lastTile);
                            selectedTile = clickedTile;
                            HighlightTile(selectedTile);
                            selectedPiece = selectedTile.GetChessPiece();
                            PositionManager.Instance.SetPieceValidPosition(selectedPiece);
                        }
                    }
                    else
                    {
                        //Add Killing Enemy Functions here
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
        lastTile = null;
        isTileSelected = false;
        PositionManager.Instance.HideAllDots();
    }
}