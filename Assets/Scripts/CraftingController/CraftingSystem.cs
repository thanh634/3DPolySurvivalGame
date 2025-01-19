using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public static CraftingSystem instance {  get; private set; }

    public GameObject craftingScreenUI;
    public GameObject toolScreenUI, survivalScreenUI, refineScreenUI, constructionScreenUI;

    public List<string> inventoryItemList = new List<string>();

    public bool isOpen;

    //Category Buttons
    Button toolsButton, survivalButton, refineButton, constructionButton;

    //Craft Buttons
    Button craftPickaxeButton;
    Button craftAxeButton;
    Button craftHammerButton;
    Button craftShovelButton;

    Button craftTorchButton;
    Button craftKnifeButton;
    Button craftSpearButton;
    Button craftBottleButton;

    Button craftPlankButton;
    Button craftHardrockButton;
    Button craftRopeButton;
    Button craftLeatherButton;

    Button craftFoundationButton;
    Button craftWallButton;
    Button craftDoorwayButton;


    //Requirement Text
    TMP_Text pickaxeReq1, pickaxeReq2;
    TMP_Text axeReq1, axeReq2;
    TMP_Text hammerReq1, hammerReq2;
    TMP_Text shovelReq1, shovelReq2;

    TMP_Text knifeReq1, knifeReq2;
    TMP_Text spearReq1, spearReq2;
    TMP_Text torchReq1, torchReq2;
    TMP_Text bottleReq1, bottleReq2, bottleReq3;

    TMP_Text plankReq1;
    TMP_Text hardrockReq1;
    TMP_Text ropeReq1;
    TMP_Text leatherReq1;

    TMP_Text foundationReq1;
    TMP_Text wallReq1;
    TMP_Text doorwayReq1, doorwayReq2;

    //All Blueprints
    public CraftableBlueprint pickaxeBlueprint = new CraftableBlueprint("Pickaxe", 2, "Rock", 3, "Stick", 3);
    public CraftableBlueprint axeBlueprint = new CraftableBlueprint("Axe", 2, "Rock", 3, "Stick", 3);
    public CraftableBlueprint hammerBlueprint = new CraftableBlueprint("Hammer", 2, "Rock", 4, "Stick", 2);
    public CraftableBlueprint shovelBlueprint = new CraftableBlueprint("Shovel", 2, "Rock", 2, "Stick", 4);

    public CraftableBlueprint knifeBlueprint = new CraftableBlueprint("Knife", 2, "Hard Rock", 1, "Stick", 2);
    public CraftableBlueprint torchBlueprint = new CraftableBlueprint("Torch", 2, "Coal", 1, "Stick", 2);
    public CraftableBlueprint spearBlueprint = new CraftableBlueprint("Spear", 2, "Hard Rock", 2, "Stick", 5);
    public CraftableBlueprint bottleBlueprint = new CraftableBlueprint("Bottle", 3, "String", 1, "Leather", 1, "Plank", 1);

    public CraftableBlueprint plankBlueprint = new CraftableBlueprint("Plank", 1, "Log", 2);
    public CraftableBlueprint hardrockBlueprint = new CraftableBlueprint("Hard Rock", 1, "Rock", 2);
    public CraftableBlueprint ropeBlueprint = new CraftableBlueprint("Rope", 1, "String", 2);
    public CraftableBlueprint leatherBlueprint = new CraftableBlueprint("Leather", 1, "Wool", 2);

    public CraftableBlueprint foundationBlueprint = new CraftableBlueprint("Foundation", 1, "Plank", 4);
    public CraftableBlueprint wallBlueprint = new CraftableBlueprint("Wall", 1, "Plank", 2);
    public CraftableBlueprint doorwayBlueprint = new CraftableBlueprint("Doorway", 2, "Stick", 2, "Plank", 1);


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

        survivalButton = craftingScreenUI.transform.Find("Survival Button").GetComponent<Button>();
        survivalButton.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        refineButton = craftingScreenUI.transform.Find("Refine Button").GetComponent<Button>();
        refineButton.onClick.AddListener(delegate { OpenRefineCategory(); });

        constructionButton = craftingScreenUI.transform.Find("Construction Button").GetComponent<Button>();
        constructionButton.onClick.AddListener(delegate { OpenConstructionCategory(); });


        // --- TOOLS --- //

        //Pickaxe
        pickaxeReq1 = toolScreenUI.transform.Find("Pickaxe").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        pickaxeReq2 = toolScreenUI.transform.Find("Pickaxe").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftPickaxeButton = toolScreenUI.transform.Find("Pickaxe").transform.Find("CraftBtn").GetComponent<Button>();
        craftPickaxeButton.onClick.AddListener(delegate { CraftAnyItem(pickaxeBlueprint); });

        //Axe
        axeReq1 = toolScreenUI.transform.Find("Axe").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        axeReq2 = toolScreenUI.transform.Find("Axe").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftAxeButton = toolScreenUI.transform.Find("Axe").transform.Find("CraftBtn").GetComponent<Button>();
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItem(axeBlueprint); });

        //Hammer
        hammerReq1 = toolScreenUI.transform.Find("Hammer").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        hammerReq2 = toolScreenUI.transform.Find("Hammer").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftHammerButton = toolScreenUI.transform.Find("Hammer").transform.Find("CraftBtn").GetComponent<Button>();
        craftHammerButton.onClick.AddListener(delegate { CraftAnyItem(hammerBlueprint); });

        //Shovel
        shovelReq1 = toolScreenUI.transform.Find("Shovel").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        shovelReq2 = toolScreenUI.transform.Find("Shovel").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftShovelButton = toolScreenUI.transform.Find("Shovel").transform.Find("CraftBtn").GetComponent<Button>();
        craftShovelButton.onClick.AddListener(delegate { CraftAnyItem(shovelBlueprint); });

        // --- SURVIVAL --- //

        //Knife
        knifeReq1 = survivalScreenUI.transform.Find("Knife").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        knifeReq2 = survivalScreenUI.transform.Find("Knife").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftKnifeButton = survivalScreenUI.transform.Find("Knife").transform.Find("CraftBtn").GetComponent<Button>();
        craftKnifeButton.onClick.AddListener(delegate { CraftAnyItem(knifeBlueprint); });

        //Torch
        torchReq1 = survivalScreenUI.transform.Find("Torch").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        torchReq2 = survivalScreenUI.transform.Find("Torch").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftTorchButton = survivalScreenUI.transform.Find("Torch").transform.Find("CraftBtn").GetComponent<Button>();
        craftTorchButton.onClick.AddListener(delegate { CraftAnyItem(torchBlueprint); });

        //Spear
        spearReq1 = survivalScreenUI.transform.Find("Spear").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        spearReq2 = survivalScreenUI.transform.Find("Spear").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftSpearButton = survivalScreenUI.transform.Find("Spear").transform.Find("CraftBtn").GetComponent<Button>();
        craftSpearButton.onClick.AddListener(delegate { CraftAnyItem(spearBlueprint); });

        //Bottle
        bottleReq1 = survivalScreenUI.transform.Find("Bottle").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        bottleReq2 = survivalScreenUI.transform.Find("Bottle").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();
        bottleReq3 = survivalScreenUI.transform.Find("Bottle").transform.Find("Requirement").GetChild(2).GetComponent<TMP_Text>();

        craftBottleButton = survivalScreenUI.transform.Find("Bottle").transform.Find("CraftBtn").GetComponent<Button>();
        craftBottleButton.onClick.AddListener(delegate { CraftAnyItem(bottleBlueprint); });

        // --- REFINE --- //

        //Hard Rock
        hardrockReq1 = refineScreenUI.transform.Find("Hard Rock").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftHardrockButton = refineScreenUI.transform.Find("Hard Rock").transform.Find("CraftBtn").GetComponent<Button>();
        craftHardrockButton.onClick.AddListener(delegate { CraftAnyItem(hardrockBlueprint); });

        //Plank
        plankReq1 = refineScreenUI.transform.Find("Plank").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftPlankButton = refineScreenUI.transform.Find("Plank").transform.Find("CraftBtn").GetComponent<Button>();
        craftPlankButton.onClick.AddListener(delegate { CraftAnyItem(plankBlueprint); });

        //Leather
        leatherReq1 = refineScreenUI.transform.Find("Leather").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftLeatherButton = refineScreenUI.transform.Find("Leather").transform.Find("CraftBtn").GetComponent<Button>();
        craftLeatherButton.onClick.AddListener(delegate { CraftAnyItem(leatherBlueprint); });

        //Rope
        ropeReq1 = refineScreenUI.transform.Find("Rope").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftRopeButton = refineScreenUI.transform.Find("Rope").transform.Find("CraftBtn").GetComponent<Button>();
        craftRopeButton.onClick.AddListener(delegate { CraftAnyItem(ropeBlueprint); });

        // --- CONSTRUCTION --- //

        //Foundation
        foundationReq1 = constructionScreenUI.transform.Find("Foundation").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftFoundationButton = constructionScreenUI.transform.Find("Foundation").transform.Find("CraftBtn").GetComponent<Button>();
        craftFoundationButton.onClick.AddListener(delegate { CraftAnyItem(foundationBlueprint); });

        //Wall
        wallReq1 = constructionScreenUI.transform.Find("Wall").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();

        craftWallButton = constructionScreenUI.transform.Find("Wall").transform.Find("CraftBtn").GetComponent<Button>();
        craftWallButton.onClick.AddListener(delegate { CraftAnyItem(wallBlueprint); });

        //Doorway
        doorwayReq1 = constructionScreenUI.transform.Find("Doorway").transform.Find("Requirement").GetChild(0).GetComponent<TMP_Text>();
        doorwayReq2 = constructionScreenUI.transform.Find("Doorway").transform.Find("Requirement").GetChild(1).GetComponent<TMP_Text>();

        craftDoorwayButton = constructionScreenUI.transform.Find("Doorway").transform.Find("CraftBtn").GetComponent<Button>();
        craftDoorwayButton.onClick.AddListener(delegate { CraftAnyItem(doorwayBlueprint); });

    }

    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems();

        if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.C)) && !isOpen && !ConstructionManager.instance.inConstructionMode)
        {

            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;

        }
        else if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.C)) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            closeAllCategories();

            if (!InventorySystem.instance.isOpen)
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

    private void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
    }

    private void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
    }

    private void OpenConstructionCategory()
    {
        craftingScreenUI.SetActive(false);
        constructionScreenUI.SetActive(true);
    }

    private void CraftAnyItem(CraftableBlueprint blueprintToCraft)
    {
        //SoundManager.instance.PlayCraftSound();

        // Remove resources from inventory
        switch (blueprintToCraft.numOfRequirements)
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
        int log_count = 0;
        int string_count = 0;
        int wool_count = 0;
        int coal_count = 0;

        int plank_count = 0;
        int hard_rock_count = 0;
        int rope_count = 0;
        int leather_count = 0;

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
                case "Log":
                    log_count++;
                    break;
                case "String":
                    string_count++;
                    break;
                case "Wool":
                    wool_count++;
                    break;
                case "Coal":
                    coal_count++;
                    break;
                case "Plank":
                    plank_count++;
                    break;
                case "Hard Rock":
                    hard_rock_count++;
                    break;
                case "Rope":
                    rope_count++;
                    break;
                case "Leather":
                    leather_count++;
                    break;
            }
        }

        // ---- PICKAXE ---- //
        pickaxeReq1.text = "3 Rock [" + rock_count + "]";
        pickaxeReq2.text = "3 Stick [" + stick_count + "]";
        craftPickaxeButton.gameObject.SetActive(rock_count >= 3 && stick_count >= 3);

        // ---- AXE ---- //
        axeReq1.text = "3 Rock [" + rock_count + "]";
        axeReq2.text = "3 Stick [" + stick_count + "]";
        craftAxeButton.gameObject.SetActive(rock_count >= 3 && stick_count >= 3);

        // ---- HAMMER ---- //
        hammerReq1.text = "4 Rock [" + rock_count + "]";
        hammerReq2.text = "2 Stick [" + stick_count + "]";
        craftHammerButton.gameObject.SetActive(rock_count >= 4 && stick_count >= 2);

        // ---- SHOVEL ---- //
        shovelReq1.text = "2 Rock [" + rock_count + "]";
        shovelReq2.text = "4 Stick [" + stick_count + "]";
        craftShovelButton.gameObject.SetActive(rock_count >= 2 && stick_count >= 4);

        // ---- KNIFE ---- //
        knifeReq1.text = "1 Hard Rock [" + hard_rock_count + "]";
        knifeReq2.text = "2 Stick [" + stick_count + "]";
        craftKnifeButton.gameObject.SetActive(hard_rock_count >= 1 && stick_count >= 2);

        // ---- TORCH ---- //
        torchReq1.text = "1 Coal [" + coal_count + "]";
        torchReq2.text = "2 Stick [" + stick_count + "]";
        craftTorchButton.gameObject.SetActive(coal_count >= 1 && stick_count >= 2);

        // ---- SPEAR ---- //
        spearReq1.text = "2 Hard Rock [" + hard_rock_count + "]";
        spearReq2.text = "5 Stick [" + stick_count + "]";
        craftSpearButton.gameObject.SetActive(hard_rock_count >= 2 && stick_count >= 5);

        // ---- BOTTLE ---- //
        bottleReq1.text = "1 String [" + string_count + "]";
        bottleReq2.text = "1 Leather [" + leather_count + "]";
        bottleReq3.text = "1 Plank [" + plank_count + "]";
        craftBottleButton.gameObject.SetActive(string_count >= 1 && leather_count >= 1 && plank_count >= 1);

        // ---- PLANK ---- //
        plankReq1.text = "2 Log [" + log_count + "]";
        craftPlankButton.gameObject.SetActive(log_count >= 2);

        // ---- ROPE ---- //
        ropeReq1.text = "2 String [" + string_count + "]";
        craftRopeButton.gameObject.SetActive(string_count >= 2);

        // ---- LEATHER ---- //
        leatherReq1.text = "2 Wool [" + wool_count + "]";
        craftLeatherButton.gameObject.SetActive(wool_count >= 2);

        // ---- HARD ROCK ---- //
        hardrockReq1.text = "2 Rock [" + rock_count + "]";
        craftHardrockButton.gameObject.SetActive(rock_count >= 2);

        // ---- FOUNDATION ---- //
        foundationReq1.text = "4 Plank [" + plank_count + "]";
        craftFoundationButton.gameObject.SetActive(plank_count >= 4);

        // ---- WALL ---- //
        wallReq1.text = "2 Plank [" + plank_count + "]";
        craftWallButton.gameObject.SetActive(plank_count >= 2);

        // --- DOORWAY --- //
        doorwayReq1.text = "2 Stick [" + stick_count + "]";
        doorwayReq2.text = "1 Plank [" + plank_count + "]";
        craftDoorwayButton.gameObject.SetActive(stick_count >= 2 && plank_count >= 1);
    }

    public void closeAllCategories()
    {
        toolScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        constructionScreenUI.SetActive(false);
    }
}
