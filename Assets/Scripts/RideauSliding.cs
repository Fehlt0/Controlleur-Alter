using System;
using System.Collections;
using UnityEngine;

public class RideauSliding : MonoBehaviour
{
    [SerializeField] private Transform targetG;
    [SerializeField] private Transform targetD;
    private Vector3 targetGInit;
    private Vector3 targetDInit;
    private Vector3 targetGToGo;
    private Vector3 targetDToGo;

    [SerializeField] private GameObject RideauG;
    [SerializeField] private GameObject RideauD;

    private bool isClosing = false;
    private bool isTimerRunning = false;

    private void Start()
    {
        targetGInit = RideauG.transform.position;
        targetDInit = RideauD.transform.position;
        targetGToGo = targetGInit;
        targetDToGo = targetDInit;
    }

    void Update()
    {
        SlideRideau();
    }

    public void SwitchTargetToCenter()
    {
        targetGToGo = targetG.position;
        targetDToGo = targetD.position;
        isClosing = true;
        isTimerRunning = false;
    }

    private void SlideRideau()
    {
        float speed = 3f;

        RideauG.transform.position = Vector3.MoveTowards(RideauG.transform.position, targetGToGo, speed * Time.deltaTime);
        RideauD.transform.position = Vector3.MoveTowards(RideauD.transform.position, targetDToGo, speed * Time.deltaTime);

        if (isClosing && !isTimerRunning && (Vector3.Distance(RideauG.transform.position, targetGToGo) < 0.01f || Vector3.Distance(RideauD.transform.position, targetDToGo) < 0.01f))
        {
            StartCoroutine(RideauTimer());
        }
    }

    private IEnumerator RideauTimer()
    {
        isTimerRunning = true; 
        yield return new WaitForSeconds(2f);

        targetGToGo = targetGInit;
        targetDToGo = targetDInit;

        isClosing = false;
        isTimerRunning = false;

        if (GameManager.instance.gameState == GameManager.GameState.waitingPlayer)
        {
            UIManager.instance.TimerStart();
        }
        else
        {
            GameManager.instance.gameState = GameManager.GameState.waitingPlayer;
            Destroy(ComboManager.instance.player1.gameObject);
            Destroy(ComboManager.instance.player2.gameObject);
        }
    }
}
