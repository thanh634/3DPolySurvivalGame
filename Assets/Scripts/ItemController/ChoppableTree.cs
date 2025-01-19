using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public bool canBeChopped;
    public float treeMaxHealth;

    public float treeHealth;
    public Animator animator;

    private void Start()
    {
        treeHealth = treeMaxHealth;
        animator = transform.parent.GetComponent<Animator>();
    }

    public void GetHit(int hp)
    {
        if (canBeChopped)
        {

            animator.SetTrigger("shake");
            treeHealth -= hp;



            if (treeHealth <= 0)
            {
                TreeIsChopped();
            }
        }
    }


    void TreeIsChopped()
    {
        Vector3 treePosition = transform.position;

        Destroy(transform.parent.gameObject);
        SelectionManager.instance.selectedTree = null;
        SelectionManager.instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("Models/ChoppedTree"),
            new Vector3(treePosition.x, treePosition.y, treePosition.z), Quaternion.Euler(0, 0, 0));

    }

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalStateSystem.instance.resourceHealth = treeHealth;
            GlobalStateSystem.instance.resourceMaxHealth = treeMaxHealth;
        }
    }
}
