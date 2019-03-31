using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerClear : StateMachineBehaviour
{
    [SerializeField] float triggerClearNormalizedTime = 0.75f;
    [SerializeField] string excludeParam = "";
    PlayerMain playerMain;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMain = animator.GetComponent<PlayerMain>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < triggerClearNormalizedTime)
        {
            foreach (string parameter in playerMain.animParameterList)
            {
                if(parameter != excludeParam)
                    animator.ResetTrigger(parameter);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
