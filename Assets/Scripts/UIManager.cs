using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Sprite finishedComboImage;
    
    [SerializeField] private List<Image> comboUIPlayer1;
    [SerializeField] private List<Image> comboUIPlayer2;

    [SerializeField] private Image timerImage;
    [SerializeField] private List<Sprite> listTimerSprite;

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

    public void UpdateInput(int input, int player)
    {
        switch (player)
        {
            case 0:
                comboUIPlayer1[input].sprite = finishedComboImage;
                break;
            case 1:
                comboUIPlayer2[input].sprite = finishedComboImage;
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
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(TimerStarting(i));
        }
    }

    private IEnumerator TimerStarting(int time)
    {
        yield return new WaitForSeconds(1f);
        if (time == 3)
        {
            timerImage.gameObject.SetActive(false);
            GameManager.instance.gameState = GameManager.GameState.Playing;
        }
        timerImage.sprite = listTimerSprite[time];
    }
}
