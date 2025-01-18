using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    public void AddToInventory()
    {

        InventorySystem.instance.AddToInventory(ItemName);

        if(!InventorySystem.instance.isFull)
        {
            Debug.Log($"{ItemName} added to inventory.");
            Destroy(gameObject);
        }

    }
}