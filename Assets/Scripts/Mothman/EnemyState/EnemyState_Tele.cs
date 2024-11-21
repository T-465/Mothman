using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Tele : IEnemyState
{
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Tele State");

    }

    public void Update(Mothman moth)
    {
        moth.teleporting = true;
        moth.transform.LookAt(moth.player);
        moth.agent.speed = 0.3f;

        if (moth.teleportcoroutinework == false)
        {
            moth.StartTeleport();
        }
        if (moth.player == null) return;
        if (moth.flashlight.flashlighton == true)
        {
            moth.SetState(new EnemyState_Chase());
        }
    }

    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Idle State");
    }

}
