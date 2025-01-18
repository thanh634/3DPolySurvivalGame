using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

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


                if (selectionTransform.GetComponent<InteractableObject>())
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
                    interaction_Info_UI.SetActive(false);
                    pickUpisVisible = false;
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
}