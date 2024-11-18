using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Chase : MonoBehaviour
{
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Chase State");
    }
    public void Update(Mothman moth)
    {
        moth.transform.position = Vector3.MoveTowards(
        moth.transform.position,
        moth.player.position,
        moth.speed * Time.deltaTime
        );
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.chaseRange)
        {
            enemy.SetState(new EnemyState_Idle());
        }
    }
    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Chase State");
    }
}
