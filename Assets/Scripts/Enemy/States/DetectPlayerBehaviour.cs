using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerBehaviour : StateMachineBehaviour
{
    GameObject target;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = ReferenceManager.instance.player;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("DistanceFromPlayer", Vector3.Distance(animator.transform.position, target.transform.position));
    }
}
