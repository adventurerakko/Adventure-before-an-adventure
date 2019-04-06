using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunStateAnim : StateMachineBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float turnSpeed = 90f;

    CharacterController characterController;
    PlayerMain playerMain;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        characterController = animator.GetComponent<CharacterController>();
        playerMain = animator.GetComponent<PlayerMain>();
        playerMain.currentStateName = "Run";
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMain.currentStateName != "Run")
            return;
        Vector3 movementVector = new Vector3(animator.GetFloat("Horizontal"), 0, animator.GetFloat("Vertical"));
        Quaternion cameraRotation = Quaternion.Euler(0, playerMain.cameraMain.transform.rotation.eulerAngles.y, 0);
        characterController.Move(cameraRotation * Vector3.ClampMagnitude(movementVector, 1) * movementSpeed * Time.deltaTime);
        if (animator.GetBool("IsMoving") && !animator.GetBool("IsLockedOn"))
        {
            characterController.transform.rotation = Quaternion.Slerp(playerMain.characterController.transform.rotation, Quaternion.LookRotation(cameraRotation * movementVector), Time.deltaTime * turnSpeed);
        }
    }
}