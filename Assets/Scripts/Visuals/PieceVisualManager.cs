using System;
using UnityEngine;

public class PieceVisualManager : MonoBehaviour
{
    [SerializeField] private Sprite whiteKingSprite;
    [SerializeField] private Sprite whiteQueenSprite;

    [SerializeField] private Sprite blackKingSprite;
    [SerializeField] private Sprite blackQueenSprite;

    [SerializeField] private Sprite whitePawnSprite;
    [SerializeField] private Sprite blackPawnSprite;

    [SerializeField] private Sprite whiteRookSprite;
    [SerializeField] private Sprite blackRookSprite;

    [SerializeField] private Sprite whiteBishopSprite;
    [SerializeField] private Sprite blackBishopSprite;

    [SerializeField] private Sprite whiteLeftKnightSprite;
    [SerializeField] private Sprite whiteRightKnightSprite;

    [SerializeField] private Sprite blackLeftKnightSprite;
    [SerializeField] private Sprite blackRightKnightSprite;

    public Sprite GetKingSprite => GameManager.Instance.IsWhite ? whiteKingSprite : blackKingSprite;
    public Sprite GetQueenSprite => GameManager.Instance.IsWhite ? whiteQueenSprite: blackQueenSprite;
    public Sprite GetPawnSprite => GameManager.Instance.IsWhite ? whitePawnSprite: blackPawnSprite;

    public Sprite GetRookSprite => GameManager.Instance.IsWhite ? whiteRookSprite: blackRookSprite;

    public Sprite GetBishopSprite => GameManager.Instance.IsWhite ? whiteBishopSprite: blackBishopSprite;

    public Sprite GetLeftKnightSprite => GameManager.Instance.IsWhite ? whiteLeftKnightSprite: blackLeftKnightSprite;
    public Sprite GetRightKnightSprite => GameManager.Instance.IsWhite ? whiteRightKnightSprite:blackRightKnightSprite;
}