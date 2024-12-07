using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyState
{
    // Mothmans attack state that implements a jumpscare, lowers health and begins an attack delay
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Attack State");

        moth.StartJumpscare();
    }

    public void Update(Mothman moth)
    {
        moth.teleporting = false;
        Debug.Log("Attacking");

        // Switch back to Tele State if the jumpscare is finished
        if (moth.jumpScareImg.enabled ==false)
        {
            moth.SetState(new EnemyState_Tele());
        }

    }
    public void Exit(Mothman moth)
    {
        moth.StartAttackDelay();
        Debug.Log("Exiting Attack State");

    }


}
