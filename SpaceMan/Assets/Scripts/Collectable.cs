using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType{
    healthPotion, 
    manaPotion,
    coin
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.coin;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    bool hasBeenCollected = false;
    public int value = 1;
    GameObject player;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled =  true;
        hasBeenCollected = false;
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled =  false;
    }

    void Collect()
    {   // Esconde el item que se a recolectado
        Hide();
        hasBeenCollected = true;

        switch(this.type)
        {
            case CollectableType.healthPotion:
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
            case CollectableType.coin:
                GameManager.sharedInstance.CollectObject(this);
                break;
            
        }
    }
    // Detecta cuando el jugador toma un objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }
}
