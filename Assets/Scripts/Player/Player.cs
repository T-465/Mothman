using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] public int playerHealth = 3;
    [SerializeField] public float speed;

    public CharacterController cc;

    public PlayerInput playerinput;

    public UIGameEnds uiGameEnds;
    public AudioSource moth1;
    public AudioSource moth2;
    private void Awake()
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
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            uiGameEnds.OnGameOver();
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
}







