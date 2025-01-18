using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text thirstCounter;

    public GameObject playerState;

    private float currentThirst, maxThirst;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentThirst = PlayerState.instance.currentThirst;
        maxThirst = PlayerState.instance.maxThirst;

        float fillValue = currentThirst / maxThirst;
        slider.value = fillValue;

        thirstCounter.text = currentThirst.ToString() + "/" + maxThirst.ToString();
    }
}
