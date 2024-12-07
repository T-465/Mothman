using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Tele : IEnemyState
{
    // Mothmans Teleporting state in which it will teleport to specified areas that change as the player progresses through the TeleCollisons script

    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Tele State");

    }

    public void Update(Mothman moth)
    {

        moth.teleporting = true;
        moth.transform.LookAt(moth.player);
        moth.agent.speed = 0.3f;
        moth.cameraShake.shaking = false;
        if (moth.teleportcoroutinework == false)
        {
            moth.StartTeleport();
        }
        if (moth.player == null) return;

        // Switch to chase state if the flashlight is on and the attack delay is over
        if (moth.flashlight.flashlighton == true && moth.jumpscaring == false && moth.attackDelayed == false)
        {
            moth.SetState(new EnemyState_Chase());
        }

    }

    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Idle State");
    }

}
