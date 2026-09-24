using UnityEngine;

public class OSCHandler : MonoBehaviour
{
    public void ReceiveOSCP1(float value)
    {
        Debug.Log(value);
        Debug.Log("Player1");
        if(value >= 80)
        {
            ComboManager.instance.player1.activeBaloon.Gonfler();
        }
    }
    
    public void ReceiveOSCP2(float value)
    {
        Debug.Log(value);
        Debug.Log("Player2");
        if(value >= 50)
        {
            ComboManager.instance.player2.activeBaloon.Gonfler();
        }
    }
}
