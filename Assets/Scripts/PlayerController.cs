using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Ballon activeBaloon;

    [SerializeField] private int maxHP;
    private float currentHP;

    public int playerID;
    
    [SerializeField] private Transform baloonSpawnPosition;
    [SerializeField] private Ballon baloonPrefab;
    
    private enum PlayerState
    {
        noState,
        jsp,
        jsp2,
        jsp3,
        won,
        lose
    }

    private PlayerState playerState = PlayerState.noState;
    
    private enum PlayerPumping // pimp my ride ou quoi la team
    {
        pumping,
        unpumping
    }
    
    private PlayerPumping playerPumping = PlayerPumping.unpumping;

    private void Start()
    {
        currentHP = maxHP;
        SpawnNewBaloon();
        ComboManager.instance.EnterPlayer(playerID, this);
    }

    public void SpawnNewBaloon()
    {
        activeBaloon = Instantiate(baloonPrefab, baloonSpawnPosition.position, baloonSpawnPosition.localRotation);
        activeBaloon.Init(this);
    }

    public void FormBaloon(int baloonType)
    {
        activeBaloon.ChangeForm(baloonType);
    }

    public void LoseLife(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            GameManager.instance.WinGame(playerID);
        }
        UIManager.instance.UpdateLife(currentHP,this);
    }

    public void OnLaunchBaloon(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.instance.gameState == GameManager.GameState.Playing && activeBaloon.state == Ballon.State.forme && playerState != PlayerState.won && playerState != PlayerState.lose)
        {
            activeBaloon.launchTarget = ComboManager.instance.GetOtherPlayer(playerID).gameObject.transform.position;
            activeBaloon.state = Ballon.State.launched;
        }
    }
    
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA    
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.instance.gameState == GameManager.GameState.Playing && playerState != PlayerState.won && playerState != PlayerState.lose)
        {
            activeBaloon.Gonfler();
        }
    }
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA    
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    //C'EST LA QU'IL FAUT MODIF PUR LA POMPE LA
    
    
    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.instance.gameState == GameManager.GameState.Playing && playerState != PlayerState.won && playerState != PlayerState.lose)
        {
            if (activeBaloon.state != Ballon.State.forme)
            {
                ComboManager.instance.ReceiveInput(0, playerID);
                if (activeBaloon.state == Ballon.State.gonfle)
                {
                    activeBaloon.StartModelling();
                }
            }
        }
    }
    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.instance.gameState == GameManager.GameState.Playing && playerState != PlayerState.won && playerState != PlayerState.lose)
        {
            if (activeBaloon.state != Ballon.State.forme)
            {
                ComboManager.instance.ReceiveInput(1, playerID);
                if (activeBaloon.state == Ballon.State.gonfle)
                {
                    activeBaloon.StartModelling();
                }
            }
        }
    }
    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.instance.gameState == GameManager.GameState.Playing && playerState != PlayerState.won && playerState != PlayerState.lose)
        {
            if (activeBaloon.state != Ballon.State.forme)
            {
                ComboManager.instance.ReceiveInput(2, playerID);
                if (activeBaloon.state == Ballon.State.gonfle)
                {
                    activeBaloon.StartModelling();
                }
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(activeBaloon.gameObject);
    }

    public void GetEffect(Ballon.BaloonType type, int damage)
    {
        if (type == Ballon.BaloonType.formHeal)
        {
            ComboManager.instance.GetOtherPlayer(playerID).LoseLife(damage / 2 * -1) ;
        }
    }

    public void WinLose(int i)
    {
        switch (i)
        {
            case 0 :
                playerState = PlayerState.won;
                break;
            case 1:
                playerState = PlayerState.lose;
                break;
        }
    }
}
