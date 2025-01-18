using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text healthCounter;

    private float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerState.instance.currentHealth;
        maxHealth = PlayerState.instance.maxHealth;

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        healthCounter.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
