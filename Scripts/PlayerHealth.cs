using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthSlider;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        SetHealth(health);
    }

    public void SetHealth(float health) { healthSlider.value = health;}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
