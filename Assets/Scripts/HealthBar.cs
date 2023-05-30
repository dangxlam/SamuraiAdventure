using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Health playerHealth;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null )
        {
            Debug.Log("Not found any Player Tag");
        }

        playerHealth = player.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        healthSlider.value = CalculateSliderPercentage(playerHealth.HealthLogic, playerHealth.MaxHealth);
        healthBarText.text = "HP " + playerHealth.HealthLogic + " / " + playerHealth.MaxHealth;
    }

    private void OnEnable()
    {
        playerHealth.healthChange.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerHealth.healthChange.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerHealthChanged(int currentHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        healthBarText.text = "HP " + currentHealth + " / " + maxHealth;
    }
}
