using System;
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
                }
                else
                {
                    lastTile = selectedTile;
                    selectedTile = clickedTile;
                    HighlightTile(selectedTile, lastTile);
                }
            }
        }
    }

    private void HighlightTile(Tile newTile)
    {
        newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
    }

    private void HighlightTile(Tile newTile, Tile oldTile)
    {
        if (selectedTile != null)
        {
            newTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
            oldTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                oldTile.GetComponent<TileVisuals>().GetStartColor();
        }
        // if (selectedTile != null)
        // {
        //     isTileSelected = true;
        //     selectedTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
        //     if (lastTile != null)
        //     {
        //         lastTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
        //             lastTile.GetComponent<TileVisuals>().GetStartColor();
        //     }
        // }
    }
}