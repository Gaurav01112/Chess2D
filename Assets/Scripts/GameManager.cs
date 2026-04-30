using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Turn currentTurn;

    public enum Turn
    {
        None,
        Bot,
        Player1,
        Player2,
    }

    public enum GameType
    {
        None,
        VsPlayer,
        VsBot
    }

    public Turn GetTurn => currentTurn;

    public Turn SetTurn(Turn turn)
    {
        return currentTurn = turn;
    }
}