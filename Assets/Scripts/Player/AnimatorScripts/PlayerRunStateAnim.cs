using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerRunStateAnim : StateMachineBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float lockedOnMovementSpeed = 5f;
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
        if (playerMain.currentStateName != "Run") // Do not run update code if transition is in process.
            return;
        Vector3 movementVector = new Vector3(animator.GetFloat("Horizontal"), 0, animator.GetFloat("Vertical"));
        Quaternion cameraRotation = Quaternion.Euler(0, playerMain.cameraMain.transform.rotation.eulerAngles.y, 0);

        if(animator.GetBool("IsLockedOn") || animator.GetBool("IsLockedOnWithEnemy")) // if is locked on, move slower
            characterController.Move(cameraRotation * Vector3.ClampMagnitude(movementVector, 1) * lockedOnMovementSpeed * Time.deltaTime);
        else
            characterController.Move(cameraRotation * Vector3.ClampMagnitude(movementVector, 1) * movementSpeed * Time.deltaTime);

        if (animator.GetBool("IsMoving") && !animator.GetBool("IsLockedOn"))
        {
            characterController.transform.rotation = Quaternion.Slerp(playerMain.characterController.transform.rotation, Quaternion.LookRotation(cameraRotation * movementVector), Time.deltaTime * turnSpeed);
        }
        else if (animator.GetBool("IsLockedOnWithEnemy"))
        {
            Vector3 rotVector = new Vector3(playerMain.nearestEnemy.transform.position.x - playerMain.characterController.transform.position.x, 0, playerMain.nearestEnemy.transform.position.z - playerMain.characterController.transform.position.z);
            characterController.transform.rotation = Quaternion.Slerp(playerMain.characterController.transform.rotation, Quaternion.LookRotation(rotVector), Time.deltaTime * turnSpeed);
        }
    }
}