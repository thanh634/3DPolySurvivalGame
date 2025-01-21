using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    public bool swingWait = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;
        if(Input.GetMouseButtonDown(0) && !swingWait && (!InventorySystem.instance.isOpen && !CraftingSystem.instance.isOpen && !ConstructionManager.instance.inConstructionMode && !MenuManager.Instance.isMenuOpen))
        {
            swingWait = true;
            animator.SetTrigger("isHit");

            StartCoroutine(NewSwingDelay());
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

    IEnumerator NewSwingDelay()
    {
        yield return new WaitForSeconds(1f);

        swingWait = false;
    }

    public void SwingTool() => GlobalStateSystem.instance.ChopTreeMakeYouTired();
    public void MakeSwingSound() => SoundManager.instance.PlayToolSwingSound();
}
