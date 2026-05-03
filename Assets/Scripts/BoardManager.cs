using System;
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
            int x = Mathf.RoundToInt(mousePos.x);
            int y = Mathf.RoundToInt(mousePos.y);

            Tile clickedTile = Board.Instance.GetTileAtPosition(x, y);
            
            if (clickedTile == null)
            {
                if (selectedTile != null)
                {
                    ResetTile(selectedTile);
                    selectedPiece = null;
                    selectedTile = null;
                    isTileSelected = false;
                }
                return;
            }

            if (!clickedTile.isOccupied && selectedTile == null)
            {
                return;
            }
            if (!clickedTile.isOccupied && selectedPiece != null)
            {
                selectedTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                    selectedTile.GetComponent<TileVisuals>().GetStartColor();
                selectedTile = null;
                lastTile = null;
                selectedPiece = null;
                isTileSelected = false;
                return;
            }
            if (GameManager.Instance.GetTeam != clickedTile.GetChessPiece().GetTeam )
            {
                Debug.Log(GameManager.Instance.GetTeam + " : " + clickedTile.GetChessPiece().GetTeam);

                if (selectedTile != null)
                {
                    selectedTile.GetComponent<TileVisuals>().GetStartColor();
                    ResetTile(selectedTile);
                    selectedPiece = null;
                    selectedTile = null;
                }

                if (lastTile != null)
                {
                    lastTile.GetComponent<TileVisuals>().GetStartColor();
                    lastTile = null;
                }

                isTileSelected = false;
                return;
            }

            if (!isTileSelected)
            {
                selectedTile = clickedTile;
            }

            if (clickedTile.isOccupied)
            {
                if (!isTileSelected)
                {
                    //selectedTile = clickedTile;
                    isTileSelected = true;
                    selectedPiece = clickedTile.GetChessPiece();
                    HighlightTile(selectedTile);
                    PositionManager.Instance.SetPieceValidPosition(selectedPiece);
                }
                else
                {
                    lastTile = selectedTile;
                    selectedTile = clickedTile;
                    HighlightTile(selectedTile);
                    ResetTile(lastTile);
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
        newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newTile.transform.GetComponent<TileVisuals>().GetStartColor();
    }
}