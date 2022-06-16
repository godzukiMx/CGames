using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
        healthBar,
        manaBar
}
public class PlayerBars : MonoBehaviour
{
    private Slider slider;
    public BarType type;
    // Start is called before the first frame update
    void Start()
    {   // Vida y mana iniciales
        slider = GetComponent<Slider>();
        switch(type)
        {
            case BarType.healthBar:
                slider.maxValue = PlayerController.MAX_HEALT;
                break;
            case BarType.manaBar:
                slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {   // actualizacion de vida y mana del player
        switch(type)
        {
            case BarType.healthBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealth();
                break;
            case BarType.manaBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();
                break;
        }
    }
}
