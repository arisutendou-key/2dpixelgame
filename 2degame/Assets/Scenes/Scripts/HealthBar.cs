using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Slider damageslider;
    public static HealthBar instance;

    public float timeToSetDamage = 2f;
    public float timeUntilSetDamage = 0f;

    private float previousHP = 0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= 0){
            SceneManager.LoadScene("GameOver");
        }
        if (timeUntilSetDamage > 0f)
        {
            timeUntilSetDamage -= Time.deltaTime;
            damageslider.maxValue = slider.maxValue;
        } 
        if (timeUntilSetDamage <= 0f)
        {
            timeUntilSetDamage = 0f;
            damageslider.maxValue = slider.maxValue;
            damageslider.value = slider.value;
        }
    }

    public void SetMaxHealth(int health){
        slider.maxValue = 100;
        slider.value = health;
        damageslider.maxValue = slider.value;
        damageslider.value = slider.value;
        print(health);
    }

    public void SetHealth(int health){
        if (previousHP != health)
        {
            previousHP = health;
            timeUntilSetDamage = timeToSetDamage;
        }
        slider.value = health;
        print(health + " / " + slider.maxValue);
    }
}
