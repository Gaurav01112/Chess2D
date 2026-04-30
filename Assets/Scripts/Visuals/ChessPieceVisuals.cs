using System;
using UnityEngine;

public class ChessPieceVisuals : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform positionTransform;

    private void Awake()
    {
        positionTransform = transform.GetChild(1);
        positionTransform.gameObject.SetActive(false);
    }
    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}