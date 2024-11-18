using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyState
{
    Mothman moth;
    public void Enter(Mothman moth)
    {
        Debug.Log("Entering Attack State");
        moth.StartCoroutine(Jumpscare());
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
    IEnumerator Jumpscare()
    {
        IDamageable damageable = moth.player.gameObject.GetComponent<IDamageable>();
        damageable.TakeDamage(moth.damage);
        damageable.ShowHitEffect();
        moth.jumpScareImg.enabled = true;
        yield return new WaitForSeconds(1);
        moth.jumpScareImg.enabled = false;
    }
}
