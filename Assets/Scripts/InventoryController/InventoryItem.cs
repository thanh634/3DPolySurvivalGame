using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TMP_Text itemInfoUI_itemName;
    private TMP_Text itemInfoUI_itemDescription;
    private TMP_Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float caloriesEffect;
    public float hydrationEffect;

    // --- Equipping --- //
    public bool isEquippable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;
    public bool isSelected;


    public bool isUseable;

    public bool isBuildable;


    private void Start()
    {
        itemInfoUI = InventorySystem.instance.itemInfoUi;

        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponentInChildren<TMP_Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponentInChildren<TMP_Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("ItemFunctionality").GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if(isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }


    private void BuildItem()
    {
        itemInfoUI.SetActive(false);

        InventorySystem.instance.isOpen = false;
        InventorySystem.instance.inventoryScreenUI.SetActive(false);

        CraftingSystem.instance.isOpen = false;
        CraftingSystem.instance.craftingScreenUI.SetActive(false);
        CraftingSystem.instance.closeAllCategories();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //SelectionManager.instance.EnableSelection();
        SelectionManager.instance.enabled = true;

        switch(gameObject.name)
        {
            case "Foundation(Clone)":
                ConstructionManager.instance.ActivateConstructionPlacement("Foundation_Model");
                break;
            case "Foundation":
                ConstructionManager.instance.ActivateConstructionPlacement("Foundation_Model");
                break;
            case "Wall(Clone)":
                ConstructionManager.instance.ActivateConstructionPlacement("Wall_Model");
                break;
            case "Wall":
                ConstructionManager.instance.ActivateConstructionPlacement("Wall_Model");
                break;
            case "Doorway(Clone)":
                ConstructionManager.instance.ActivateConstructionPlacement("Doorway_Model");
                break;
            case "Doorway":
                ConstructionManager.instance.ActivateConstructionPlacement("Doorway_Model");
                break;
            default:
                break;
        }
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.instance.ReCalculateList();
                CraftingSystem.instance.RefreshNeededItems();
            }
        }
    }


    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }

            if (isEquippable && isInsideQuickSlot == false && EquipSystem.instance.CheckIfFull() == false)
            {
                EquipSystem.instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }

            if (isBuildable)
            {
                ConstructionManager.instance.itemToBeBuilt = gameObject;
                gameObject.SetActive(false);

                ConstructionManager.instance.constructAlertUI.SetActive(true);
                foreach (GameObject ghost in ConstructionManager.instance.allGhostsInExistence)
                {
                    if(ghost) ghost.SetActive(true);
                }

                BuildItem();
            }
        }

    }

    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        caloriesEffectCalculation(caloriesEffect);

        hydrationEffectCalculation(hydrationEffect);

    }


    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //

        float healthBeforeConsumption = PlayerState.instance.currentHealth;
        float maxHealth = PlayerState.instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.instance.SetHealth(maxHealth);
            }
            else
            {
                PlayerState.instance.SetHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        // --- Calories --- //

        float caloriesBeforeConsumption = PlayerState.instance.currentFood;
        float maxCalories = PlayerState.instance.maxFood;

        if (caloriesEffect != 0)
        {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                PlayerState.instance.SetCalories(maxCalories);
            }
            else
            {
                PlayerState.instance.SetCalories(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }


    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // --- Hydration --- //

        float hydrationBeforeConsumption = PlayerState.instance.currentThirst;
        float maxHydration = PlayerState.instance.maxThirst;

        if (hydrationEffect != 0)
        {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration)
            {
                PlayerState.instance.SetHydration(maxHydration);
            }
            else
            {
                PlayerState.instance.SetHydration(hydrationBeforeConsumption + hydrationEffect);
            }
        }
    }


}