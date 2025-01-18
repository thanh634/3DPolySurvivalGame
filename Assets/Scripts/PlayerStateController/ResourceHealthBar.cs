
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHealthBar : MonoBehaviour
{
    private Slider slider;
    private float currentHealth, maxHealth;

    public GameObject globalState;
    private void Awake()
    {
        slider = GetComponent<Slider>();

    }

    private void Update()
    {
        currentHealth = globalState.GetComponent<GlobalStateSystem>().resourceHealth;
        maxHealth = globalState.GetComponent<GlobalStateSystem>().resourceMaxHealth;

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

    }
}
