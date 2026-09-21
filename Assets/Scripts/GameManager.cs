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

    private RideauSliding rideauSliding;

    private void Start()
    {
        rideauSliding = GetComponent<RideauSliding>();
    }

    public void StartGame()
    {
        rideauSliding.SwitchTargetToCenter();
    }
}
