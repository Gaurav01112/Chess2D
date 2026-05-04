using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;
    [SerializeField] private GameObject positionPrefab;

    private Transform validPositionTransform;

    private List<Transform> dots = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPositionPrefab();
    }

    private void SpawnPositionPrefab()
    {
        GameObject G = Instantiate(positionPrefab);
        foreach (Transform child in G.transform)
        {
            dots.Add(child);
            child.gameObject.SetActive(false);
        }

        validPositionTransform = G.transform;
    }

    public void SetPieceValidPosition(ChessPiece piece)
    {
        HideAllDots();

        List<Vector2Int> validMoves = piece.GetAvailableMoves();

        for (int i = 0; i < validMoves.Count; i++)
        {
            if (i < dots.Count)
            {
                dots[i].position = new Vector3(validMoves[i].x, validMoves[i].y, -1);
                dots[i].gameObject.SetActive(true);
            }
            Debug.Log("Jay Shree Ram");
        }
    }

    public void HideAllDots()
    {
        foreach (Transform dot in dots)
        {
            dot.gameObject.SetActive(false);
        }
    }
}