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
    public bool playerDead;
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

    }
    private void OnDisable()
    {
        Tele1 = null;
        Tele2 = null;
        Tele3 = null;
        Tele4 = null;
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
    // ... (Other methods remain unchanged, including TakeDamage and Die)
    private void LocatePlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
   public void StartJumpscare()
    {
        StartCoroutine(Jumpscare());
    }

    public void StartTeleport()
    {
        StartCoroutine(Teleporter());
        
    }
    public void StopTeleport()
    {
        StopCoroutine(Teleporter());
    }
    IEnumerator Jumpscare()
    {
        IDamageable damageable = player.gameObject.GetComponent<IDamageable>();
        damageable.TakeDamage(damage);
        damageable.ShowHitEffect();
        jumpScareImg.enabled = true;
        yield return new WaitForSeconds(1);
        jumpScareImg.enabled = false;
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
}
