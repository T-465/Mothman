using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Tele : MonoBehaviour, IEnemyState
{
    Mothman moth;
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Tele State");

    }
    void Test(Mothman moth)
    {
        moth = moth;
    }
    public void Update()
    {
        moth.transform.LookAt(moth.player);
        moth.agent.speed = 0.5f;

        if (moth.teleporting && moth.teleportcoroutinework!)
        {
            StartCoroutine(Teleporter());
        }
        if (moth.player == null) return;
        if (Vector3.Distance(moth.transform.position, moth.player.position) < moth.attackRange && moth.flashlight.flashlighton == true)
        {
            moth.SetState(new EnemyState_Chase());
        }
    }
    IEnumerator Teleporter()
    {
        moth.teleportcoroutinework = true;

        moth.transform.position = moth.Tele1.transform.position;

        yield return new WaitForSeconds(3f);
        moth.transform.position = moth.Tele2.transform.position;

        yield return new WaitForSeconds(4f);
        moth.transform.position = moth.Tele3.transform.position;

        yield return new WaitForSeconds(4f);
        moth.transform.position = moth.Tele4.transform.position;
        yield return new WaitForSeconds(3f);
        moth.teleportcoroutinework = false;
    }
    public void Exit(Mothman moth)
    {
        Debug.Log("Exiting Idle State");
    }

}
