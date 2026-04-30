using System;
using UnityEngine;

public class TileVisuals : MonoBehaviour
{
    private Color startColor;

    private void Start()
    {
        startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }
    public Color GetStartColor()
    {
        return startColor;
    }
}