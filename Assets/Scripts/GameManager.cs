using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isWhite = false;

    private Team currentTeam;

    private void Awake()
    {
        Instance = this;
    }

    public Team GetTeam => currentTeam;

    public void ChangeTurn()
    {
        currentTeam = (currentTeam == Team.TeamWhite) ? Team.TeamBlack : Team.TeamWhite;
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