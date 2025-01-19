using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem instance { get; set; }

    // -- UI -- //
    private int selectedNumber = -1;

    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    public GameObject numbersHolder;

    public GameObject selectedItem;

    public GameObject toolHolder;
    private GameObject selectedItemModel;

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

    void Update()
    {
        //foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(key))
        //    {
        //        Debug.Log($"Key Pressed: {key}");
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }

    }

    void SelectQuickSlot(int number)
    {
        if(CheckIfSlotIsFull(number) == true)
        {
            if(selectedNumber != number)
            {
                selectedNumber = number;

                // Unselect previously select item
                if (selectedItem != null)
                {
                    Debug.Log(selectedItem.gameObject.name);
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                foreach (Transform child in numbersHolder.transform)
                {

                    child.transform.Find("Text").GetComponent<TMP_Text>().color = Color.gray;
                }

                selectedItem = GetSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);

                TMP_Text toBeChanged = numbersHolder.transform.Find("Number" + number).transform.Find("Text").GetComponent<TMP_Text>();
                toBeChanged.color = Color.white;
            }
            else // Try to select same slot
            {
                selectedNumber = -1;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<TMP_Text>().color = Color.gray;
                }

                if (selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }
            }

        }
    }

    bool CheckIfSlotIsFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount >0)
        {
            return true;
        }
        return false;
    }

    GameObject GetSelectedItem(int number)
    {
        return quickSlotsList[number - 1].transform.GetChild(0).gameObject;
    }

    private void SetEquippedModel(GameObject selectedItem)
    {
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>("Models/"+ selectedItemName + "_Model"),
            new Vector3(0.4f, 0.2f , 0.4f), Quaternion.Euler(0, -110f, -20f));

        selectedItemModel.transform.SetParent(toolHolder.transform, false);
    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("Quick Slot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("(Clone)", "");
        // Adding item to list
        itemList.Add(cleanName);

        InventorySystem.instance.ReCalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}