using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerBehaviour : StateMachineBehaviour
{
    GameObject player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = ReferenceManager.instance.Player;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("DistanceFromPlayer", Vector3.Distance(animator.transform.position, player.transform.position));
    }
}
