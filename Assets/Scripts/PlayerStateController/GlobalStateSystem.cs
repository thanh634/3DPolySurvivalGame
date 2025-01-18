using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateSystem : MonoBehaviour
{
    public static GlobalStateSystem instance { get; private set; }

    public float resourceHealth, resourceMaxHealth;

    public float foodSpentChoppingWood;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    public void ChopTreeMakeYouTired()
    {
        PlayerState.instance.currentFood -= foodSpentChoppingWood;
    }
}
