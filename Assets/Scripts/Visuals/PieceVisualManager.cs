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

    public Sprite GetWhiteKingSprite => whiteKingSprite;
    public Sprite GetWhiteQueenSprite => whiteQueenSprite;
    public Sprite GetBlackKingSprite => blackKingSprite;
    public Sprite GetBlackQueenSprite => blackQueenSprite;
    public Sprite GetWhitePawnSprite => whitePawnSprite;

    public Sprite GetBlackPawnSprite => blackPawnSprite;

    public Sprite GetWhiteRookSprite => whiteRookSprite;

    public Sprite GetBlackRookSprite => blackRookSprite;

    public Sprite GetWhiteBishopSprite => whiteBishopSprite;

    public Sprite GetBlackBishopSprite => blackBishopSprite;

    public Sprite GetWhiteLeftKnightSprite => whiteLeftKnightSprite;

    public Sprite GetWhiteRightKnightSprite => whiteRightKnightSprite;

    public Sprite GetBlackLeftKnightSprite => blackLeftKnightSprite;

    public Sprite GetBlackRightKnightSprite => blackRightKnightSprite;
}