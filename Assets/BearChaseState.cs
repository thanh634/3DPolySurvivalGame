using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearChaseState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stopChaseDistance = 15f;
    public float chaseSpeed = 5f;
    public float attackingDistance = 2f;

    List<Transform> waypointsList = new List<Transform>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Initialization
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if(distanceFromPlayer > stopChaseDistance)
        {
            animator.SetBool("isChasing", false);
        }

        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetTrigger("attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
