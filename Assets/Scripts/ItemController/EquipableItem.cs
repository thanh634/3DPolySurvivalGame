using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;
        if(Input.GetMouseButtonDown(0) && (!InventorySystem.instance.isOpen && !CraftingSystem.instance.isOpen && !ConstructionManager.instance.inConstructionMode))
        {

            animator.SetTrigger("isHit");

        }
    }

    public void Hit()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;
        if (selectedTree != null)
        {
            selectedTree.GetComponent<ChoppableTree>().GetHit(2);
        }
    }

    public void SwingTool() => GlobalStateSystem.instance.ChopTreeMakeYouTired();
}
