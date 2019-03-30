using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : State
{
    PlayerMain playerMain;
    public PlayerDodgeState(PlayerMain playerMain)
    {
        this.playerMain = playerMain;
    }
    public void Enter()
    {
        playerMain.animator.SetBool("IsDodging", true);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        playerMain.animator.SetBool("IsDodging", false);
    }
}
