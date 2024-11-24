using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Controller controller;
    public Slider healthSlider, batterySlider, timeSlider;
    public GameObject shopPanel;

    public int maxHealth, maxBattery, maxTime;
    int currentHealth, currentBattery, currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDefs();
    }

    void SetDefs()
    {
        currentHealth = PlayerPrefs.GetInt("health", 0);
        currentBattery = PlayerPrefs.GetInt("battery", 0);
        currentTime = PlayerPrefs.GetInt("time", 0);


        healthSlider.maxValue = maxHealth;
        batterySlider.maxValue = maxBattery;
        timeSlider.maxValue = maxTime;

        healthSlider.value = currentHealth;
        batterySlider.value = currentBattery;
        timeSlider.value = currentTime;

    }

    public void buyHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            PlayerPrefs.SetInt("health", currentHealth);
            healthSlider.value = currentHealth;
            Debug.Log("Health Upgraded");
        }
        else
        {
            Debug.Log("Health Full");
        }
    }

    public void buyBattery()
    {
        if (currentBattery < maxBattery)
        {
            currentBattery += 1;
            PlayerPrefs.SetInt("battery", currentBattery);
            batterySlider.value = currentBattery;
            Debug.Log("Battery Upgraded");
        }
        else
        {
            Debug.Log("Battery Full");
        }
    }

    public void buyTime()
    {
        if (currentTime < maxTime)
        {
            currentTime += 1;
            PlayerPrefs.SetInt("time", currentTime);
            timeSlider.value = currentTime;
            Debug.Log("Time Upgraded");
        }
        else
        {
            Debug.Log("Time Full");
        }
    }

    public void PauseShop()
    {
        shopPanel.SetActive(true);
    }

    public void ResumeShop()
    {
        shopPanel.SetActive(false);
    }
}
