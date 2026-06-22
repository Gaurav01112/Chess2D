using System;
using UnityEngine;
using UnityEngine.UI;

public class ChessManager : MonoBehaviour
{
    public static ChessManager Instance;

    private GameType gameType;

    private Team currentTeam;
    private bool isWhite = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsWhite => isWhite;

    public bool SetIsWhite(bool isWhite)
    {
        return this.isWhite == isWhite;
    }

    public enum GameType
    {
        White,
        Black,
        Random
    }

    public GameType GetGameType => gameType;

    public GameType SetGameType(GameType gameType)
    {
        return this.gameType = gameType;
    }
}