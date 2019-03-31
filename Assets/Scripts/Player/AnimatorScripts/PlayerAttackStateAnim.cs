using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateAnim : StateMachineBehaviour
{
    PlayerMain playerMain;
    CharacterController characterController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMain = animator.GetComponent<PlayerMain>();
        playerMain.currentStateName = "Attack";
        characterController = playerMain.GetComponent<CharacterController>();
        Vector3 movementVector = new Vector3(animator.GetFloat("Horizontal"), 0, animator.GetFloat("Vertical"));
        if(movementVector != Vector3.zero)
            characterController.transform.forward = Quaternion.Euler(0, playerMain.cameraMain.transform.rotation.eulerAngles.y, 0) * movementVector;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMain.currentStateName != "Attack")
            return;
    }
}
