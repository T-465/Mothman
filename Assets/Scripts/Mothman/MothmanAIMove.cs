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
    public float targetChromaticAb = 1.0f;
    #endregion

    #region Teleporting
    public bool teleporting;
    public bool teleportcoroutinework;
    public Transform mothMan;
    public Transform Tele1;
    public Transform Tele2;
    public Transform Tele3;
    public Transform Tele4;
    #endregion


    #region Audio
    public AudioSource MothAttack;
    public AudioSource MothAttack2;
    public AudioSource MothDirectional;
    public AudioSource MothDirectional2;
    public AudioSource Jumpscare1;
    public AudioSource Jumpscare2;
    #endregion


    //Attacking
    public float timeBetweenAtacks;
    bool alreadyAttacked;
    public float distance;

    //States
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        jumpScareImg.enabled = false;
        vol.profile.TryGet<ChromaticAberration>(out ca);


        Tele1 = null;
        Tele2 = null;
        Tele3 = null;
        Tele4 = null;

    }
    private void OnEnable()
    {
        Tele1 = GameObject.FindWithTag("Tele1").transform;
        Tele2 = GameObject.FindWithTag("Tele2").transform;
        Tele3 = GameObject.FindWithTag("Tele3").transform;
        Tele4 = GameObject.FindWithTag("Tele4").transform;
        Teleporting();
    }
    private void OnDisable()
    {
        Tele1 = null;
        Tele2 = null;
        Tele3 = null;
        Tele4 = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Update()
    {
        if (Tele1 == null)
            Tele1 = GameObject.FindWithTag("Tele1").transform;
        
        if (Tele2 == null)
            Tele2 = GameObject.FindWithTag("Tele2").transform;
        
        if (Tele3 == null)
            Tele3 = GameObject.FindWithTag("Tele3").transform;
       
        if (Tele4 == null)
            Tele4 = GameObject.FindWithTag("Tele4").transform;
        
       
        //check for attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       if (!playerInAttackRange && !flashlight.flashlighton) teleporting = true;

       if (teleporting && !teleportcoroutinework)
       {
            Teleporting();
       }
        if (teleporting == false)teleportcoroutinework = false; StopCoroutine(Teleporter());
        

       if (!playerInAttackRange && flashlight.flashlighton) Moving();
       if (playerInAttackRange) Attack();

      

        
         distance = Vector3.Distance(player.position, transform.position);


        if (distance < 40f && distance > 39.5f && !teleporting)
        {
            Debug.Log("playsounds");
            MothAttack.Play();
            MothAttack2.Play();
        }
        if (distance > 80f && distance < 81f && teleporting)
        {
            Debug.Log("playtelesound1");
            MothDirectional.Play();
            
        }
        if (distance > 65f && distance < 66f && teleporting)
        {
            Debug.Log("playtelesound2");
       
            MothDirectional2.Play();
        }
       
    }



    private void Teleporting()
    {
        Debug.Log("Teleporting");
        teleporting = true;
        transform.LookAt(player);
        agent.speed = 0.5f;
        
        //ca.intensity.Override(currentChromaticAb);


        if (teleporting)
        {
            StartCoroutine(Teleporter());
        }
        
    }
 
    private void Moving()
    {

        Debug.Log("Moving");
        agent.speed = 20;
      
        teleporting = false;
        transform.LookAt(player);
        agent.SetDestination(player.position);
        //ca.intensity.Override(targetChromaticAb);
     

     
    }
    private void Attack()
    {
       
            Debug.Log("playsounds");
            Jumpscare1.Play();
            Jumpscare2.Play();
        
        jumpScareImg.enabled = true;
      

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

    IEnumerator Teleporter()
    {
        teleportcoroutinework = true;
        mothMan.transform.position = Tele1.transform.position;
    
        yield return new WaitForSeconds(2f);
        mothMan.transform.position = Tele2.transform.position;
     
        yield return new WaitForSeconds(3f);
        mothMan.transform.position = Tele3.transform.position;
   
        yield return new WaitForSeconds(2f);
        mothMan.transform.position = Tele4.transform.position;
        yield return new WaitForSeconds(3f);
        teleportcoroutinework = false;  
    }

}
