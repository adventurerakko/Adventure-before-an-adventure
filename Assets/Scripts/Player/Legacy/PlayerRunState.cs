using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerRunState : State
{
    /*Vector3 movementVector = Vector3.zero;
    PlayerMain playerMain;
    public PlayerRunState(PlayerMain playerMain)
    {
        this.playerMain = playerMain;
    }
    

    public void Enter()
    {
        playerMain.animator.SetBool("IsMoving", true);
    }
    public void Execute()
    {
        movementVector = playerMain.playerInput.CheckMove();
        if (movementVector == Vector3.zero)
        {
            playerMain.stateManager.ChangeState(new PlayerIdleState(playerMain));
        }

        else
        {
            Quaternion cameraRotation = Quaternion.Euler(0, playerMain.cameraMain.transform.rotation.eulerAngles.y, 0);
            playerMain.characterController.Move(cameraRotation * movementVector * playerMain.movementSpeed * Time.deltaTime);
            playerMain.characterController.transform.rotation = Quaternion.Slerp(playerMain.characterController.transform.rotation, Quaternion.LookRotation(cameraRotation * movementVector), Time.deltaTime * playerMain.turnSpeed);
        }
    }
    public void Exit()
    {
        playerMain.animator.SetBool("IsMoving", false);
    }*/
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
