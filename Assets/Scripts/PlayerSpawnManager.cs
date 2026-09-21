using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    private PlayerInputManager inputManager;

    [SerializeField] private GameObject player2;
    
    [SerializeField] private List<Transform> positionList;
    private int playerCount;

    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (positionList.Count > 0)
        {
            playerInput.transform.position = positionList[playerCount].position;
            playerInput.transform.rotation = positionList[playerCount].rotation;
        }

        playerCount++;
        if (playerCount == 1)
        {
            inputManager.playerPrefab = player2;
        }
        else if (playerCount == 2)
        {
            Destroy(ComboManager.instance.player1.gameObject);
            playerCount = 0;
        }
    }
}
