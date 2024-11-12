using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] public float playerHealth = 3;
    [SerializeField] public float speed;
    [SerializeField] private float gravity = -9.81f;


 
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


    }
}







