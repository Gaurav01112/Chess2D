using System;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;
    [SerializeField] private GameObject positionPrefab;
    
    private Transform validPositionTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnValidPositionPrefab();
    }

    private void SpawnValidPositionPrefab()
    {
        GameObject G = Instantiate(positionPrefab);
        G.SetActive(false);
        validPositionTransform = G.transform;
    }

    public void SetPieceValidPosition(ChessPiece piece)
    {
        validPositionTransform.SetParent(piece.transform);
        validPositionTransform.transform.position = piece.transform.position;
    }
    
    
    
    public Transform GetValidPositionTransform()
    {
        return validPositionTransform;
    }
}