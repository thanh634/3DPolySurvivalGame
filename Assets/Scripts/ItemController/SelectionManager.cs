using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    public GameObject interaction_Info_UI;
    TMP_Text interaction_text;

    public Image centerPointIcon;
    public Image pickUpIcon;

    public bool pickUpisVisible;

    public GameObject selectedTree;
    public GameObject selectedAnimal;
    public Image chopHolder;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(CraftingSystem.instance.isOpen || InventorySystem.instance.isOpen)
        {
            centerPointIcon.gameObject.SetActive(false);
            pickUpIcon.gameObject.SetActive(false);
            interaction_Info_UI.SetActive(false);
        }

        else
        {
            if (Physics.Raycast(ray, out hit, 2f))
            {
                var selectionTransform = hit.transform;
                InteractableObject thisObj = selectionTransform.GetComponent<InteractableObject>();
                Animal thisAnimal = selectionTransform.GetComponent<Animal>(); 


                ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();

                if (choppableTree)
                {
                    choppableTree.canBeChopped = true;
                    selectedTree = choppableTree.gameObject;
                    chopHolder.gameObject.SetActive(true);
                }
                else
                {
                    if(selectedTree != null)
                    {
                        selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
                        selectedTree = null;
                        chopHolder.gameObject.SetActive(false);
                    }
                }


                if (thisObj)
                {
                    interaction_text.text = thisObj.GetItemName();
                    interaction_Info_UI.SetActive(true);

                    if (thisObj.CompareTag("Pickable"))
                    {
                        pickUpisVisible = true;
                        centerPointIcon.gameObject.SetActive(false);
                        pickUpIcon.gameObject.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            SoundManager.instance.PlayPickUpItemSound();
                            thisObj.AddToInventory();
                        }
                    }
                    else
                    {
                        centerPointIcon.gameObject.SetActive(true);
                        pickUpIcon.gameObject.SetActive(false);
                        pickUpisVisible = false;
                    }
                }
                else // if hit a not InteractableObject
                {
                    centerPointIcon.gameObject.SetActive(true);
                    pickUpIcon.gameObject.SetActive(false);
                    pickUpisVisible = false;
                }

                if (thisAnimal)
                {
                    interaction_Info_UI.SetActive(true);
                    selectedAnimal = thisAnimal.gameObject;

                    if (thisAnimal.isDead)
                    {
                        interaction_text.text = "Loot";

                        centerPointIcon.gameObject.SetActive(false);
                        pickUpIcon.gameObject.SetActive(true);
                        pickUpisVisible = true;

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Lootable lootable = thisAnimal.GetComponent<Lootable>();

                            SoundManager.instance.PlayPickUpItemSound();
                            Loot(lootable);
                        }

                    }
                    else
                    {
                        thisAnimal.playerInRange = true;

                        interaction_text.text = thisAnimal.name;

                        centerPointIcon.gameObject.SetActive(true);
                        pickUpIcon.gameObject.SetActive(false);
                        pickUpisVisible = false;

                        if (Input.GetMouseButtonDown(0) && EquipSystem.instance.IsHoldingWeapon() && !EquipSystem.instance.SwingLock())
                        {
                            StartCoroutine(DealDamageTo(thisAnimal, 0.3f, EquipSystem.instance.GetWeaponDamage()));
                        }
                    }
                }
                else
                {
                    if (selectedAnimal != null)
                    {
                        selectedAnimal.gameObject.GetComponent<Animal>().playerInRange = false;
                        selectedAnimal = null;
                    }
                }

                if(!thisObj && !thisAnimal)
                {
                    interaction_text.text = "";
                    interaction_Info_UI.SetActive(false);
                }
            }
            else // if not hit anything
            {
                centerPointIcon.gameObject.SetActive(true);
                pickUpIcon.gameObject.SetActive(false);
                interaction_Info_UI.SetActive(false);
                chopHolder.gameObject.SetActive(false);
                pickUpisVisible = false;
                if( selectedTree!= null ) 
                    selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
            }
        }


    }

    private void Loot(Lootable lootable)
    {
        if (!lootable.wasLootCalculated)
        {
            List<LootRecieved> receivedLoot = new List<LootRecieved>();

            foreach (LootPossibility loot in lootable.possibleLoot)
            {
                var lootAmmount = UnityEngine.Random.Range(loot.ammountMin, loot.ammountMax + 1);
                if(lootAmmount != 0)
                {
                    LootRecieved lt = new LootRecieved();
                    lt.item = loot.item;
                    lt.ammount = lootAmmount;

                    receivedLoot.Add(lt);
                }
            }

            lootable.finalLoot = receivedLoot;
            lootable.wasLootCalculated = true;
        }

        Vector3 lootSpawnPosition = lootable.gameObject.transform.position;

        foreach(LootRecieved loot in lootable.finalLoot)
        {
            for (int i = 0; i < loot.ammount; i++)
            {
                GameObject lootSpawn = Instantiate(Resources.Load<GameObject>("Models/Loots/" + loot.item.name + "_Model"), 
                    new Vector3(lootSpawnPosition.x, lootSpawnPosition.y + 0.3f, lootSpawnPosition.z), 
                    Quaternion.Euler(0,0, 0));
            }
        }

        Destroy(lootable.gameObject);
    }

    IEnumerator DealDamageTo(Animal thisAnimal, float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        thisAnimal.TakeDamage(damage);
    }
}