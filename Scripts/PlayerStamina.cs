using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Slider StaminaSlider;

    public void SetMaxStamina(float stamina)
    {
        StaminaSlider.maxValue = stamina;
        SetStamina(stamina);
    }

    public void SetStamina(float health) { StaminaSlider.value = health; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
