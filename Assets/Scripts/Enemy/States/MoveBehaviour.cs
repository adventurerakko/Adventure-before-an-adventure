using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
public class MoveBehaviour : StateMachineBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] bool moveTowardsTarget = false;

    NavMeshAgent navMeshAgent;
    GameObject player = null;
    Vector3 targetVector = Vector3.zero;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Assert.IsNotNull(navMeshAgent);
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        player = ReferenceManager.instance.Player;
        targetVector = (player.transform.position - animator.transform.position).normalized;
        animator.transform.rotation = Quaternion.LookRotation(targetVector);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!moveTowardsTarget)
            navMeshAgent.Move(animator.transform.forward * movementSpeed * Time.deltaTime);
        else
        {
            navMeshAgent.Move(targetVector * movementSpeed * Time.deltaTime);
        }
    }
}