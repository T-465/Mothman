using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;


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
    public bool teleporting;
    public bool teleportcoroutinework;
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

    private void Start()
    {
        SetState(new EnemyState_Tele());
        Invoke("LocatePlayer", 1f);
    }
    private void Update()
    {
        currentState?.Update(this);
    }
    public void SetState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
    public string GetCurrentStateName()
    {
        return currentState != null ? currentState.GetType().Name.Replace("Enemy", "") : "No State";
    }
    // ... (Other methods remain unchanged, including TakeDamage and Die)
    private void LocatePlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
