using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeStateAnim : StateMachineBehaviour
{
    [SerializeField] AnimationCurve animationCurve = null;
    [SerializeField] float dodgeDistance = 10;

    CharacterController characterController;
    Vector3 movementVector = Vector3.zero;
    PlayerMain playerMain;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterController = animator.GetComponent<CharacterController>();
        playerMain = animator.GetComponent<PlayerMain>();
        playerMain.currentStateName = "Dodge";

        if (animator.GetBool("IsMoving"))
        {
            Quaternion cameraRotation = Quaternion.Euler(0, playerMain.cameraMain.transform.rotation.eulerAngles.y, 0);
            movementVector = new Vector3(animator.GetFloat("Horizontal"), 0, animator.GetFloat("Vertical"));
            movementVector = cameraRotation * movementVector;
            characterController.transform.rotation = Quaternion.LookRotation(movementVector);
        }
        else
            movementVector = characterController.transform.forward;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMain.currentStateName != "Dodge")
            return;
        characterController.Move(Vector3.ClampMagnitude(movementVector, 1) * dodgeDistance * Time.deltaTime * animationCurve.Evaluate(stateInfo.normalizedTime));
    }
}