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
        if (moth.player == null) return;
        if (Vector3.Distance(moth.transform.position, moth.player.position) < moth.attackRange)
        {
            moth.SetState(new EnemyState_Chase());
        }
    }
    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Idle State");
    }

}
