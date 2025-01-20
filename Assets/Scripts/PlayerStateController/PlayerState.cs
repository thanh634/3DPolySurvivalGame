using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance { get; private set; }

    float distanceTraveled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    // ---- Player Food ---- //
    public float currentFood;
    public float maxFood;

    // ---- Player Thirst ---- //
    public float currentThirst;
    public float maxThirst;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentFood = maxFood;
        currentThirst = maxThirst;

        StartCoroutine(increaseThirst());
    }

    // Update is called once per frame

    bool isThirsty = true;
    IEnumerator increaseThirst()
    {
        while(isThirsty)
        {
            currentThirst -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        distanceTraveled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if(distanceTraveled >= 10)
        {
            distanceTraveled = 0;
            currentFood -= 1;
            currentThirst -= 1;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHealth -= 10;
        }
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
    public void SetCalories(float newCalories)
    {
        currentFood = newCalories;
    }
    public void SetHydration(float newHydration)
    {
        currentThirst = newHydration;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {

        }
        else
        {

        }
    }
}
