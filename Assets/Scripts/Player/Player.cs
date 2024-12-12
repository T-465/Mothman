using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamageable
{
    // Script controlling aspects of the player such as movement, health and damage


    [SerializeField] public int playerHealth = 3;
    [SerializeField] public float speed;
    public CharacterController cc;
    public PlayerInput playerinput;
    public AudioSource breathing;
    public bool isBreathing;


    //Damage/Hit Variables
    public UI ui;
    public AudioSource moth1;
    public AudioSource moth2;

    public void Awake()
    {
        cc = GetComponent<CharacterController>();

    }
    private void FixedUpdate()
    {
        #region PlayerMove

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.SimpleMove(move * speed * Time.deltaTime);


        #endregion

    }

    private void Update()
    {
        // Tired breathing when player isnt moving
        if (Input.anyKey)
        {
            isBreathing = false;
            breathing.Stop();
        }
        else if (!isBreathing)
        {
            Breath();
        }
      
    }
    void Breath()
    {
        isBreathing = true;
        breathing.Play();

    }

    #region Damage
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            ui.OnGameOver();
            
        }
        else if (playerHealth > 0)
        {
            ui.OnHit();
        }

    }
    public void OnGameOver() 
    { 
      moth1.Play();
      moth2.Play();
    
    }
    public void ShowHitEffect()
    {
        
    }
    #endregion
}
