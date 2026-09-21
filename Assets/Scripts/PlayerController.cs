using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Ballon activeBaloon;

    [SerializeField] private int maxHP;
    private float currentHP;

    [SerializeField] private int playerID;
    
    [SerializeField] private Transform baloonSpawnPosition;
    [SerializeField] private Ballon baloonPrefab;

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
    }

    public void OnLaunchBaloon(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            activeBaloon.launchTarget = ComboManager.instance.GetOtherPlayer(playerID).gameObject.transform.position;
            activeBaloon.state = Ballon.State.launched;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            activeBaloon.Gonfler();
        }
    }
    public void OnLeft(InputAction.CallbackContext context)
    {
        
        if (context.started)
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
        if (context.started)
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
        if (context.started)
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
}
