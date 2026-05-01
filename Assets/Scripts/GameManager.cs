using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isWhite = false;

    private Team currentTeam;
    private Turn currentTurn;

    private void Awake()
    {
        Instance = this;
    }

    public enum Turn
    {
        TeamWhite,
        TeamBlack
    }

    public Team GetTeam => currentTeam;

    public Team SetTeam(Team team)
    {
        return currentTeam = team;
    }

    public Turn GetTurn => currentTurn;

    public Turn SetTurn(Turn turn)
    {
        return currentTurn = turn;
    }

    public bool IsWhite => isWhite;

    public bool SetIsWhite(bool isWhite)
    {
        return this.isWhite = isWhite;
    }
}

public enum Team
{
    TeamWhite = 0,
    TeamBlack = 1
}