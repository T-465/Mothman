using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Chase :  IEnemyState
{
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Chase State");
    }
    public void Update(Mothman moth)
    {
        Debug.Log("Chasing");
        moth.agent.speed = 25;

     
        moth.transform.LookAt(moth.player);
        moth.agent.SetDestination(moth.player.position);

        if (moth.flashlight.flashlighton == false)
        {
            moth.SetState(new EnemyState_Tele());
        }

        if (moth.playerInAttackRange)
        {
            moth.StopTeleport();
            moth.SetState(new EnemyState_Attack());
        }
    }
    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Chase State");
    }
}
