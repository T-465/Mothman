using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyState
{

    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Attack State");

        moth.StartJumpscare();
    }

    public void Update(Mothman moth)
    {
        Debug.Log("Attacking");
        if (moth.jumpScareImg.enabled ==false)
        {
            moth.SetState(new EnemyState_Tele());
        }

    }
    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Attack State");
    }


}
