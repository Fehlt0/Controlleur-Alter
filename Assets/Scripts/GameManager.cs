using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    
    public enum GameState
    {
        waitingPlayer,
        Playing,
        Restarting
    }

    public GameState gameState;

    private RideauSliding rideauSliding;

    private void Start()
    {
        rideauSliding = GetComponent<RideauSliding>();
        gameState = GameState.waitingPlayer;
    }

    public void StartGame()
    {
        rideauSliding.SwitchTargetToCenter();
    }

    public void WinGame(int player)
    {
        switch (player)
        {
            //0 le joueur 2 gagne, 1 le joueur 1 gagne
            case 0 :
                ComboManager.instance.player2.WinLose(0);
                ComboManager.instance.player1.WinLose(1);
                UIManager.instance.WinUIDisplay(player);
                break;
            case 1:
                ComboManager.instance.player2.WinLose(1);
                ComboManager.instance.player1.WinLose(0);
                UIManager.instance.WinUIDisplay(player);
                break;
        }
    }

    public void RestartGame()
    {
        Debug.Log("Game Restarting");
        rideauSliding.SwitchTargetToCenter();
        UIManager.instance.ResetLife();
    }
}
