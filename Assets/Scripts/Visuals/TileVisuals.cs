using System;
using TMPro;
using UnityEngine;

public class TileVisuals : MonoBehaviour
{
    public static TileVisuals Instance;

    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private TextMeshProUGUI numText;

    private string[] tileAlphabets = new[] { "a", "b", "c", "d", "e", "f", "g", "h" };
    private string[] tileNum = new[] { "1", "2", "3", "4", "5", "6", "7", "8" };

    private Color startColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }

    public Color GetStartColor()
    {
        return startColor;
    }

    public GameObject GetLetterTextObj()
    {
        return letterText.gameObject;
    }

    public TextMeshProUGUI SetLetterTextObj(string letter)
    {
        letterText.text = letter;
        return letterText;
    }

    public GameObject GetNumTextObj()
    {
        return numText.gameObject;
    }

    public TextMeshProUGUI SetNumTextObj(string num)
    {
        numText.text = num;
        return numText;
    }

    public string[] GetTileAlphabet()
    {
        return tileAlphabets;
    } public string[] GetTileNum()
    {
        return tileNum;
    }
}