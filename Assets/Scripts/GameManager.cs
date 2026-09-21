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
}
