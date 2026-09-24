using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private int player1StarterCombo;
    private int player2StarterCombo;

    private bool player1IsInCombo;
    private bool player2IsInCombo;

    private int player1ComboPlace;
    private int player2ComboPlace;
    
    public List<int> player1ComboToDo;
    public List<int> player2ComboToDo;

    public PlayerController player1;
    public PlayerController player2;

    public PlayerController GetOtherPlayer(int input)
    {
        if (input == 0)
        {
            return player2;
        }
        else
        {
            return player1;
        }
    }

    public void EnterPlayer(int player, PlayerController playerController)
    {
        switch (player)
        {
            case 0:
                player1 = playerController;
                break;
            case 1:
                player2 = playerController;
                break;
        }
    }

    public void ReceiveInput(int input, int player)
    {
        if (player == 0)
        {
            if (!player1IsInCombo && player1.activeBaloon.state == Ballon.State.gonfle)
            {
                player1IsInCombo = true;
                player1StarterCombo = input;
                for (int i = 0; i < 5; i++)
                {
                    player1ComboToDo.Add(Random.Range(0, 3));
                }
                UIManager.instance.SpawnInput(player1ComboToDo, player);
            }
            else if(player1.activeBaloon.state == Ballon.State.modele)
            {
                if (input == player1ComboToDo[player1ComboPlace])
                {
                    UIManager.instance.UpdateInput(player1ComboPlace, player, input);
                    player1ComboPlace++;
                    if (player1ComboPlace == player1ComboToDo.Count)
                    {
                        player1.FormBaloon(player1StarterCombo);
                        player1IsInCombo = false;
                        player1ComboToDo = new List<int>();
                        player1ComboPlace = 0;
                        UIManager.instance.ResetInput(0);
                    }
                }
            }
        }
        else
        {
            if (!player2IsInCombo && player2.activeBaloon.state == Ballon.State.gonfle)
            {
                player2IsInCombo = true;
                player2StarterCombo = input;
                for (int i = 0; i < 5; i++)
                {
                    player2ComboToDo.Add(Random.Range(0, 3));
                }
                UIManager.instance.SpawnInput(player2ComboToDo, player);
            }
            else if(player2.activeBaloon.state == Ballon.State.modele)
            {
                if (input == player2ComboToDo[player2ComboPlace])
                {
                    UIManager.instance.UpdateInput(player2ComboPlace, player, input);
                    player2ComboPlace++;
                    if (player2ComboPlace == player2ComboToDo.Count)
                    {
                        player2.FormBaloon(player2StarterCombo);
                        player2IsInCombo = false;
                        player2ComboToDo = new List<int>();
                        player2ComboPlace = 0;
                        UIManager.instance.ResetInput(1);
                    }
                }
            }
        }
    }
}
