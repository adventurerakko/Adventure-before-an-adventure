using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    PlayerMain playerMain;
    public PlayerIdleState(PlayerMain playerMain)
    {
        this.playerMain = playerMain;
    }
    public void Enter()
    {
    }

    public void Execute()
    {
        if(playerMain.playerInput.CheckMove() != Vector3.zero)
        {
            playerMain.stateManager.ChangeState(new PlayerRunState(playerMain));
        }
    }

    public void Exit()
    {
    }
}