using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveBehaviour : StateMachineBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] bool moveTowardsTarget = false;
    GameObject target = null;
    Vector3 targetVector = Vector3.zero;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = ReferenceManager.instance.player;
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        targetVector = (target.transform.position - animator.transform.position).normalized;
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