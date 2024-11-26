using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;


public class Mothman : MonoBehaviour
{
    public NavMeshAgent agent;

    public int damage = 1;
    private IEnemyState currentState;
    public Transform player;


    public LayerMask whatIsGround, whatIsPlayer;

    public Flashlight flashlight;
    public Player playerScript;
    public Image jumpScareImg;

    #region GlobalVolume
    public Volume vol;
    private ChromaticAberration ca;
    public float currentChromaticAb = 0.43f;
    public float targetChromaticAb = 1.0f;
    #endregion

    #region Teleporting
    public bool teleportcoroutinework;
    public bool teleporting;
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
    
    public bool jumpscaring;

    
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        jumpScareImg.enabled = false;
        IDamageable damageable = player.gameObject.GetComponent<IDamageable>();

        Tele1 = null;
        Tele2 = null;
        Tele3 = null;
        Tele4 = null;
    }
    private void Start()
    {

        SetState(new EnemyState_Tele());
        Invoke("LocatePlayer", 1f);
    }
    private void OnEnable()
    {
        Tele1 = GameObject.FindWithTag("Tele1").transform;
        Tele2 = GameObject.FindWithTag("Tele2").transform;
        Tele3 = GameObject.FindWithTag("Tele3").transform;
        Tele4 = GameObject.FindWithTag("Tele4").transform;
        SetState(new EnemyState_Tele());

    }
    private void OnDisable()
    {
        teleportcoroutinework = false;
        teleporting = false;
        jumpscaring = false;

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
    private void Update()
    {
        currentState?.Update(this);
        if (Tele1 == null)
            Tele1 = GameObject.FindWithTag("Tele1").transform;

        if (Tele2 == null)
            Tele2 = GameObject.FindWithTag("Tele2").transform;

        if (Tele3 == null)
            Tele3 = GameObject.FindWithTag("Tele3").transform;

        if (Tele4 == null)
            Tele4 = GameObject.FindWithTag("Tele4").transform;

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
    }
    public void SetState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
    public string GetCurrentStateName()
    {
        return currentState != null ? currentState.GetType().Name.Replace("Moth", "") : "No State";
    }
   
    private void LocatePlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    #region Start/Stop Methods
    public void StartJumpscare()
    {
        StartCoroutine(Jumpscare());
    }

    public void StartTeleport()
    {
        StartCoroutine(Teleporter());
        StartCoroutine(TeleSounds());
    }
    public void StopTeleport()
    {
        StopCoroutine(Teleporter());
    }
    public void StartWarningSound()
    {
        StartCoroutine(WarningSound());
    }
    public void StopWarningSound()
    {
        StopCoroutine(WarningSound());
    }
    #endregion

    #region Ienumerators
    IEnumerator Jumpscare()
    {
        Debug.Log("playsounds");
        Jumpscare1.Play();
        Jumpscare2.Play();
        jumpscaring = true;
        SetState(new EnemyState_Tele());
        jumpScareImg.enabled = true;
        IDamageable damageable = player.gameObject.GetComponent<IDamageable>();
        damageable.TakeDamage(damage);
        damageable.ShowHitEffect();
        yield return new WaitForSeconds(1);
        jumpScareImg.enabled = false;
        jumpscaring = false;
    }
    IEnumerator Teleporter()
    {
        teleportcoroutinework = true;

        transform.position = Tele1.transform.position;

        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => teleporting == true);
    
        transform.position = Tele2.transform.position;

        yield return new WaitForSeconds(4f);
        yield return new WaitUntil(() => teleporting == true);
        transform.position = Tele3.transform.position;

        yield return new WaitForSeconds(4f);
        yield return new WaitUntil(() => teleporting == true);
        transform.position = Tele4.transform.position;
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => teleporting == true);
        teleportcoroutinework = false;
    }
    IEnumerator TeleSounds()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => teleporting == true);
        Debug.Log("playtelesound1");
            MothDirectional.Play();
        yield return new WaitForSeconds(5f);
        yield return new WaitUntil(() => teleporting == true);
        Debug.Log("playtelesound2");
            MothDirectional2.Play();
        
        yield return new WaitForSeconds(7f);
        yield return new WaitUntil(() => teleporting == true);
    }
    IEnumerator WarningSound()
    {
        yield return new WaitForSeconds(5f);
        yield return new WaitUntil(() => teleporting == false && jumpscaring == false && playerScript.playerHealth == 1);
        MothAttack.Play();
        MothAttack2.Play();

    }
#endregion
}
