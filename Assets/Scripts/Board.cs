using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    private const int ROW_MAX = 8;
    private const int COL_MAX = 8;

    [SerializeField] private Tile whiteTilePrefab;
    [SerializeField] private Tile greenTilePrefab;

    private Tile[,] board = new Tile [COL_MAX, ROW_MAX];

    private Transform tileParent;

    private List<Tile> allTiles = new List<Tile>();

    private void Awake()
    {
        Instance = this;
        tileParent = GetComponent<Transform>();
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < ROW_MAX; i++)
        {
            for (int j = 0; j < COL_MAX; j++)
            {
                Tile G;
                if ((i + j) % 2 == 0)
                {
                    G = Instantiate(greenTilePrefab, tileParent);
                }
                else
                {
                    G = Instantiate(whiteTilePrefab, tileParent);
                }

                G.transform.position = new Vector3(j * 1f, i * 1f, 0);

                G.name = (i * ROW_MAX + j).ToString();

                board[j, i] = G;
                G.SetTilePosition(new Vector2Int(j, i));
                allTiles.Add(G);
            }
        }

        SetBoardText();
        SetTileData();
    }

    private void SetBoardText()
    {
        for (int i = 0; i < allTiles.Count; i++)
        {
            var pos = allTiles[i].GetTilePosition();
            var visuals = allTiles[i].GetComponent<TileVisuals>();

            bool showLetter = pos.x >= 0 && pos.y <= 0;
            bool showNum = pos.x <= 0 && pos.y >= 0;

            visuals.GetLetterTextObj().SetActive(showLetter);
            visuals.GetNumTextObj().SetActive(showNum);
            if (showLetter)
            {
                int letterIndex = Mathf.RoundToInt(pos.x);
                visuals.SetLetterTextObj(visuals.GetTileAlphabet()[letterIndex]);
            }

            if (showNum)
            {
                int numIndex = Mathf.RoundToInt(pos.y);
                visuals.SetNumTextObj(visuals.GetTileNum()[numIndex]);
            }
        }
    }

    private void SetTileData()
    {
        for (int i = 0; i < allTiles.Count; i++)
        {
            var visuals = allTiles[i].GetComponent<TileVisuals>();
            int col = i % 8;
            int row = i / 8;

            visuals.SetCurrentLetter(visuals.GetTileAlphabet()[col]);
            visuals.SetCurrentNum(visuals.GetTileNum()[row]);
        }
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        if (x >= 0 && x < COL_MAX && y >= 0 && y < ROW_MAX)
        {
            return board[x, y];
        }

        return null;
    }

    public List<Tile> GetAllTilesList()
    {
        return allTiles;
    }
}