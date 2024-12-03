using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Chase :  IEnemyState
{
    // Mothmans Chase state in which he will chase the player if their flashlight is on and will shake the  players camera as it approaches as a warning

    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Chase State");

    }
    public void Update(Mothman moth)
    {
        
        moth.teleporting = false;
        Debug.Log("Chasing");
        moth.agent.speed = 30;

    
        moth.transform.LookAt(moth.player);
        moth.agent.SetDestination(moth.player.position);

        if (moth.flashlight.flashlighton == false)
        {
            moth.SetState(new EnemyState_Tele());
        }

        if (moth.playerInAttackRange == true && moth.jumpscaring == false)
        {
            moth.SetState(new EnemyState_Attack());

        }
       if (moth.playerInWarningRange == true) 
       { 
         moth.cameraShake.shaking = true;
       }
        else
        {
            moth.cameraShake.shaking = false;
        }
    }
    public void Exit(Mothman moth)
    {

        Debug.Log("Exiting Chase State");
    }
}
