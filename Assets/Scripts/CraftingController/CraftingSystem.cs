using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public static CraftingSystem instance {  get; private set; }

    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;

    public List<string> inventoryItemList = new List<string>();

    public bool isOpen;

    //Category Buttons
    Button toolsButton;

    //Craft Buttons
    Button craftPickaxeButton;
    Button craftHardrockButton;

    //Requirement Text
    TMP_Text hardrockReq1;
    TMP_Text pickaxeReq1, pickaxeReq2;

    //All Blueprints
    public CraftableBlueprint pickaxeBlueprint = new CraftableBlueprint("Pickaxe", 2, "Rock", 3, "Stick", 3);
    public CraftableBlueprint hardrockBlueprint = new CraftableBlueprint("Hard Rock", 1, "Rock", 2);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        toolsButton = craftingScreenUI.transform.Find("Tools Button").GetComponent<Button>();

        toolsButton.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Hard Rock
        hardrockReq1 = toolScreenUI.transform.Find("Hard Rock").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftHardrockButton = toolScreenUI.transform.Find("Hard Rock").transform.Find("CraftBtn").GetComponent<Button>();
        craftHardrockButton.onClick.AddListener(delegate { CraftAnyItem(hardrockBlueprint); });

        //Pickaxe
        pickaxeReq1 = toolScreenUI.transform.Find("Pickaxe").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        pickaxeReq2 = toolScreenUI.transform.Find("Pickaxe").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftPickaxeButton = toolScreenUI.transform.Find("Pickaxe").transform.Find("CraftBtn").GetComponent<Button>();
        craftPickaxeButton.onClick.AddListener(delegate { CraftAnyItem(pickaxeBlueprint); });

    }

    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems();

        if ((Input.GetKeyDown(KeyCode.O)) && !isOpen)
        {

            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;

        }
        else if ((Input.GetKeyDown(KeyCode.O)) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);

            if(!InventorySystem.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;

            }


            isOpen = false;
        }
    }

    private void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    private void CraftAnyItem(CraftableBlueprint blueprintToCraft)
    {


        // Remove resources from inventory
        switch(blueprintToCraft.numOfRequirements)
        {
            case 1:
                InventorySystem.instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
                break;
            case 2:
                InventorySystem.instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
                InventorySystem.instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
                break;
            case 3:
                InventorySystem.instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
                InventorySystem.instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
                InventorySystem.instance.RemoveItem(blueprintToCraft.req3, blueprintToCraft.req3Amount);
                break;
            default:
                Debug.Log("Is this a blueprint ?");
                break;
        }    

        // Add item to inventory
        InventorySystem.instance.AddToInventory(blueprintToCraft.craftableName);

        // Refresh List
        StartCoroutine(calculate());

    }

    public IEnumerator calculate()
    {
        yield return 0;

        InventorySystem.instance.ReCalculateList();

        RefreshNeededItems();
    }

    public void RefreshNeededItems()
    {
        int rock_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Rock":
                    rock_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;

            }
        }

        // ---- HARD ROCK ---- //

        hardrockReq1.text = "2 Rock [" + rock_count + "]";

        if (rock_count >= 2)
        {
            craftHardrockButton.gameObject.SetActive(true);
        }
        else
        {
            craftHardrockButton.gameObject.SetActive(false);
        }

        // ---- PICKAXE ---- //

        pickaxeReq1.text = "3 Rock [" + rock_count + "]";
        pickaxeReq2.text = "3 Stick [" + stick_count + "]";

        if (rock_count >= 3 && stick_count >= 3)
        {
            craftPickaxeButton.gameObject.SetActive(true);
        }
        else
        {
            craftPickaxeButton.gameObject.SetActive(false);
        }
    }

}
