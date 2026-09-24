using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public List<Sprite> listFleche; //fleche gauche = 0; fleche up = 1; fleche right = 2

    [SerializeField] private Sprite noComboImage;
    [SerializeField] private List<Sprite> finishedComboImage;
    
    [SerializeField] private List<Image> comboUIPlayer1;
    [SerializeField] private List<Image> comboUIPlayer2;
    [SerializeField] private List<Image> player1LifeUI;
    [SerializeField] private List<Image> player2LifeUI;
    private int player1LifeCount;
    private int player2LifeCount;
    [SerializeField] private Sprite heartFilled;
    [SerializeField] private Sprite heartEmpty;

    
    [SerializeField] private Image timerImage;
    [SerializeField] private List<Sprite> listTimerSprite;

    [SerializeField] private Image player1Win;
    [SerializeField] private Image player2Win;


    public void SpawnInput(List<int> listInput, int player)
    {
        List<Image> listToSpawn = new List<Image>();
        switch (player)
        {
            case 0:
                listToSpawn = comboUIPlayer1;
                break;
            case 1:
                listToSpawn = comboUIPlayer2;
                break;
        }
        for (int i = 0; i < listInput.Count; i++)
        {
            Sprite spriteToSpawn;
            switch (listInput[i])
            {
                case 0:
                    spriteToSpawn = listFleche[0];
                    break;
                case 1:
                    spriteToSpawn = listFleche[1];
                    break;
                default:
                    spriteToSpawn = listFleche[2];
                    break;
            }

            listToSpawn[i].sprite = spriteToSpawn;
        }
    }

    public void UpdateInput(int input, int player, int PlayerInput)
    {
        switch (player)
        {
            case 0:
                comboUIPlayer1[input].sprite = finishedComboImage[PlayerInput];
                break;
            case 1:
                comboUIPlayer2[input].sprite = finishedComboImage[PlayerInput];
                break;
        }
    }

    public void ResetInput(int player)
    {
        switch (player)
        {
            case 0:
                for (int i = 0; i < comboUIPlayer1.Count; i++)
                {
                    comboUIPlayer1[i].sprite = noComboImage;
                }
                break;
            case 1:
                for (int i = 0; i < comboUIPlayer2.Count; i++)
                {
                    comboUIPlayer2[i].sprite = noComboImage;
                }
                break;
        }
    }

    public void TimerStart()
    {
        //le timer se reset a zéro quand est désactivé, donc liste de sprite  :: 1,2,"go",0
        timerImage.gameObject.SetActive(true);
        
        StartCoroutine(TimerStarting());
    }

    private IEnumerator TimerStarting()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                timerImage.gameObject.SetActive(false);
                GameManager.instance.gameState = GameManager.GameState.Playing;
            }
            timerImage.sprite = listTimerSprite[i];
        
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateLife(float life, PlayerController playerController)
    {
        float hpLost = 100 - life;
        if (hpLost <= 0)
        {
            hpLost = 0;
        }
        if (playerController == ComboManager.instance.player1)
        {
            for (int i = 0; i < hpLost; i+=10)
            {
                player1LifeUI[i / 10].sprite = heartEmpty;
            }
        }
        else
        {
            for (int i = 0; i < hpLost; i+=10)
            {
                player2LifeUI[i / 10].sprite = heartEmpty;
            }
        }
    }

    public void WinUIDisplay(int player)
    {
        switch (player)
        {
            case 0:
                player2Win.gameObject.SetActive(true);
                StartCoroutine(WinTimer());
                break;
            case 1:
                player1Win.gameObject.SetActive(true);
                StartCoroutine(WinTimer());
                break;
        }
    }

    private IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(5f);
        player2Win.gameObject.SetActive(false);
        player1Win.gameObject.SetActive(false);
        GameManager.instance.RestartGame();
    }

    public void ResetLife()
    {
        foreach (var VARIABLE in player1LifeUI)
        {
            VARIABLE.sprite = heartFilled;
        }
        foreach (var VARIABLE in player2LifeUI)
        {
            VARIABLE.sprite = heartFilled;
        }
    }
}
