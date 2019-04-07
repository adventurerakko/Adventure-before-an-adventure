using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayerBehaviour : StateMachineBehaviour
{
    GameObject player;
    [SerializeField] float turnSpeed = 0.1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = ReferenceManager.instance.Player;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(player.transform.position - animator.transform.position), turnSpeed);
    }
}
