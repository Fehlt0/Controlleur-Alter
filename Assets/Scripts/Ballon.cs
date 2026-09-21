using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour
{
    [SerializeField] private PlayerController playerRef;

    [SerializeField] private List<Sprite> spritesBaloon;

    private SpriteRenderer spriteRenderer;
    
    private enum BaloonType
    {
        basic,
        form1,
        form2,
        form3
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
        if (state == State.gonfle)
        {
            inflation += 0.2f;
            transform.localScale = new Vector3(inflation/1.5f, inflation, inflation);
            if (inflation > 2.5)
            {
                float proba = Random.value;
                if (proba >= 0.7)
                {
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
                baloonType = BaloonType.form2;
                break;
            case 2:
                baloonType = BaloonType.form3;
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
        float speed = 5f;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController enemy = other.GetComponent<PlayerController>();
            enemy.LoseLife(inflation);
        }
    }
}
