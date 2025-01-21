using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearWalkState : StateMachineBehaviour
{
    float timer;
    public float walkingTime = 15f;

    Transform player;
    NavMeshAgent agent;

    public float detectionAreaRadius = 15f;
    public float walkSpeed = 2.5f;
    
    List<Transform> waypointsList = new List<Transform>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Initialization
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;

        // -- Get all waypoint and move to the first
        GameObject waypointsCluster = animator.GetComponent<AIWaypoints>().waypointCluster;
        foreach (Transform t in waypointsCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 firstPostion = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(firstPostion);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- if agent arrived to waypoint -> move to next
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log(waypointsList.Count);
            foreach(Transform t in waypointsList)
                Debug.Log(t.position);
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
            

        }

        // -- transition to idle state
        timer += Time.deltaTime;
        if(timer > walkingTime)
        {
            animator.SetBool("isWalking", false);
        }

        // -- transition to chase state
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer < detectionAreaRadius && PlayerState.instance.currentHealth > 0)
        {
            animator.SetBool("isChasing", true);
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
