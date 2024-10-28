using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MothmanAIMove : MonoBehaviour
{
   public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Flashlight flashlight;
    public Player playerScript;
    public bool playerDead;

   

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


    }
    private void Moving()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);

    }
    private void Attack()
    {
        //Enemy look at player
       transform.LookAt(player);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAtacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
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
