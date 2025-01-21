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

    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip playerPainSound;
    [SerializeField] private Light directionalLight; // Drag the light in the inspector
    [SerializeField] private float deathLightIntensity = 0.02f;

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
        currentHealth = Mathf.Max(currentHealth - damage, 0); // Clamp health to 0

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
        else
        {
            PlaySound(playerPainSound);
             // Change the light tint to red when hurt
            if (directionalLight != null)
            {
                StartCoroutine(ChangeLightColorTemporarily(directionalLight, Color.red, 0.5f)); // Change to red for 0.5 seconds
            }
        }
    }

    private void HandleDeath()
    {
        PlaySound(playerDeathSound);
        if (directionalLight != null)
        {
            StartCoroutine(DecreaseLightIntensityOverTime(directionalLight, deathLightIntensity, 2f)); // Example: 1 second duration
        }
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator DecreaseLightIntensityOverTime(Light light, float targetIntensity, float duration)
    {
        float startIntensity = light.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        light.intensity = targetIntensity; // Ensure it's exactly at the target value
    }


    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, playerBody.transform.position);
            Debug.Log("Playing sound: " + clip.name);
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1);

        // Reset player position
        playerBody.transform.position = new Vector3(26, 4, 69);

        // Wait for 1 seconds
        yield return new WaitForSeconds(1);

        // Implement respawn logic here
        Debug.Log("Player has been respawned.");
        
        // Reset player stats
        currentHealth = maxHealth;
        currentFood = maxFood;
        currentThirst = maxThirst;


        // Reset light intensity
        if (directionalLight != null)
        {
            Debug.Log("Resetting light intensity.");
            directionalLight.intensity = 1f; // Reset to original intensity
        }
    }

    private IEnumerator ChangeLightColorTemporarily(Light light, Color targetColor, float duration)
    {
        Color originalColor = light.color; // Store the original color
        light.color = targetColor; // Change to the target color

        yield return new WaitForSeconds(duration); // Wait for the specified duration

        light.color = originalColor; // Reset to the original color
    }

}
