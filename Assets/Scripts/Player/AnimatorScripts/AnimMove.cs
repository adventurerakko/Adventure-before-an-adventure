using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMove : StateMachineBehaviour
{
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float distance;
    CharacterController characterController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterController = animator.GetComponent<CharacterController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterController.Move(distance * Time.deltaTime * characterController.transform.forward * animationCurve.Evaluate(stateInfo.normalizedTime));
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
