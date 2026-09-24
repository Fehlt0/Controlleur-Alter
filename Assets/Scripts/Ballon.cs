using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour
{
    [SerializeField] private PlayerController playerRef;

    [SerializeField] private List<Sprite> spritesBaloon;
    
    [SerializeField] private List<Sprite> spriteBaloonGonfle;
    private int gonfleState = 0;

    private SpriteRenderer spriteRenderer;
    
    public enum BaloonType
    {
        basic,
        form1,
        formHeal,
        formLent
    }

    public enum State
    {
        gonfle,
        modele,
        forme,
        launched
    }

    private BaloonType baloonType;
    public State state;

    public Vector3 launchTarget;
    
    private float inflation = 1f;
    
    public void Init(PlayerController playerController)
    {
        playerRef = playerController;
        baloonType = BaloonType.basic;
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.gonfle;
    }

    private void Update()
    {
        if (state == State.launched)
        {
            AutoLaunch(launchTarget);
        }
    }


    public void Gonfler()
    {
        Debug.Log("Gonfler");
        if (state == State.gonfle)
        {
            inflation += 1f;
            if(inflation % 3 == 0 && gonfleState + 1 < spriteBaloonGonfle.Count)
            {
                gonfleState++;
                spriteRenderer.sprite = spriteBaloonGonfle[gonfleState];
            }
            if (inflation > 15f)
            {
                float proba = Random.value;
                if (proba <= (inflation / 7) / 5)
                {
                    playerRef.LoseLife(inflation);
                    DestroyBaloon();
                }
            }
        }
    }

    private void DestroyBaloon()
    {
        Destroy(gameObject);
        playerRef.SpawnNewBaloon();
    }

    public void StartModelling()
    {
        state = State.modele;
    }

    public void ChangeForm(int input)
    {
        state = State.forme;
        switch (input)
        {
            case 0:
                baloonType = BaloonType.form1;
                break;
            case 1:
                baloonType = BaloonType.formHeal;
                break;
            case 2:
                baloonType = BaloonType.formLent;
                break;
        }
        SwitchSprite(input);
    }

    private void SwitchSprite(int input)
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.sprite = spritesBaloon[input];
    }

    public void AutoLaunch(Vector3 target)
    {
        float speed;
        if (baloonType == BaloonType.formLent)
        {
            speed = 2f;
        }
        else
        {
            speed = 4f;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && state == State.launched)
        {
            Debug.Log("jvihrehv");
            PlayerController enemy = other.GetComponent<PlayerController>();
            if (baloonType == BaloonType.formLent)
            {
                enemy.LoseLife(inflation * 1.25f);
            }
            else
            {
                enemy.LoseLife(inflation * 0.8f);
            }
            
            if (baloonType == BaloonType.formHeal)
            {
                ComboManager.instance.GetOtherPlayer(enemy.playerID).LoseLife(-inflation / 2);
            }
            
            DestroyBaloon();
        }
    }
}
