using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject itemInfoUi;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject slotToEquip;

    public bool isOpen;
    public bool isFull;

    // Pickup Popup
    public GameObject pickUpNotification;
    public TMP_Text pickUpText;
    public Image pickUpImage;

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

    void Start()
    {
        isOpen = false;
        isFull = false;
        
        Cursor.visible = false;
        PopulateSlotList();
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab)) && !isOpen && !ConstructionManager.instance.inConstructionMode)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;

        }
        else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab)) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            itemInfoUi.SetActive(false);

            if (!CraftingSystem.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;

            }

            isOpen = false;
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            foreach (Transform secondChild in child.transform)
            {
                if (secondChild.CompareTag("Inventory Slot"))
                {
                    slotList.Add(secondChild.gameObject);
                }
            }
        }
    }

    public void AddToInventory(string itemName)
    {
        slotToEquip = FindNextEmptySlot();
        if(!slotToEquip)
        {
            StartCoroutine(TriggerPickUpPopUp("The inventory is full", null));
            isFull = true;
        }
        else
        {
            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), slotToEquip.transform.position, slotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(slotToEquip.transform);

            itemList.Add(itemName);

            StartCoroutine(TriggerPickUpPopUp(itemName, itemToAdd.GetComponent<Image>().sprite));

            isFull = false;

            ReCalculateList();
            CraftingSystem.instance.RefreshNeededItems();
        }
    }

    IEnumerator TriggerPickUpPopUp(string itemName, Sprite itemSprite)
    {

        if(itemSprite ==null)
        {
            pickUpText.text = itemName;
        }
        else
        {
            pickUpText.text = "pick up " + itemName + " x 1";
            pickUpImage.sprite = itemSprite;
        }


        pickUpNotification.SetActive(true);

        yield return new WaitForSeconds(2f);

        pickUpNotification.SetActive(false);
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for(var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if ((slotList[i].transform.GetChild(0).name == nameToRemove+"(Clone)" || slotList[i].transform.GetChild(0).name == nameToRemove) && counter != 0)
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);

                    counter--;

                }
            }
        }

        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItems();
    }

    public void ReCalculateList()
    {
        itemList.Clear();

        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;

                string trueName = name.Replace("(Clone)","");

                itemList.Add(trueName);
            }
        }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }

}