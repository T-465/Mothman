using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MothmanAIMove : MonoBehaviour
{
   public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Flashlight flashlight;
    public Player playerScript;
    public bool playerDead;
    public Image jumpScareImg;

    #region GlobalVolume
    public Volume vol;
    private ChromaticAberration ca;
    public float currentChromaticAb = 0.43f;
    public float targetChromaticAb = 0.7f;
    #endregion

    //Teleporting


    //Attacking
    public float timeBetweenAtacks;
    bool alreadyAttacked;

    //States
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        jumpScareImg.enabled = false;
        vol.profile.TryGet<ChromaticAberration>(out ca);
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
       
        //check for attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       if (!playerInAttackRange && !flashlight.flashlighton) Teleporting();
       if (!playerInAttackRange && flashlight.flashlighton) Moving();
       if (playerInAttackRange) Attack();

 
    }

   

    private void Teleporting()
    {
        transform.LookAt(player);
        ca.intensity.Override(currentChromaticAb);

    }
    private void Moving()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
        ca.intensity.Override(targetChromaticAb);
    }
    private void Attack()
    {
        //jumpScareImg.enabled = true;
       

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAtacks);
            Teleporting();
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        jumpScareImg.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        playerScript.playerHealth -= damage;
        if (playerScript.playerHealth <= 0) Invoke(nameof(Death), .5f);
    }
    private void Death()
    {
        playerDead = true;
    }
}
