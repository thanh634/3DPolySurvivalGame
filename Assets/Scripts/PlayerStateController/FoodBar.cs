using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text foodCounter;

    public GameObject playerState;

    private float currentFood, maxFood;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFood = PlayerState.instance.currentFood;
        maxFood = PlayerState.instance.maxFood;

        float fillValue = currentFood / maxFood;
        slider.value = fillValue;

        foodCounter.text = currentFood.ToString() + "/" + maxFood.ToString();
    }
}
