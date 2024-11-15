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


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        #region PlayerMove


       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.SimpleMove(move * speed * Time.deltaTime);



        #endregion

        #region Damage

        #endregion
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Destroy(gameObject); // Destroy crate when health reaches 0
        }
    }
    public void ShowHitEffect()
    {
       
    }
}







