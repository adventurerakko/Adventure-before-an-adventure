using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimTriggerClear : StateMachineBehaviour
{
    [SerializeField] float triggerClearNormalizedTime = 0.75f;
    [SerializeField] string excludeParam = "";
    List<string> animParameterList = new List<string>();
    PlayerMain playerMain;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMain = animator.GetComponent<PlayerMain>();
        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animParameterList.Add(parameter.name);
            }
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < triggerClearNormalizedTime)
        {
            foreach (string parameter in animParameterList)
            {
                if(parameter != excludeParam)
                    animator.ResetTrigger(parameter);
            }
        }
    }
}
