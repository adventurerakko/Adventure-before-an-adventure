using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleStateAnim : StateMachineBehaviour
{
    PlayerMain playerMain;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMain = animator.GetComponent<PlayerMain>();
        playerMain.currentStateName = "Idle";
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMain.currentStateName != "Idle")
            return;
    }
}