using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    private Vector3 movement;
    public CharacterController cc;

    public PlayerInput playerinput;

    #region Door
    public DoorOpen doorOpen;

    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

    }
    

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && doorOpen.doorclosed == true)
        {
            Debug.Log("E pressed");
            StartCoroutine(doorOpen.OpenDoor());
        }
        else if (Input.GetKeyDown(KeyCode.E) && doorOpen.dooropen == true)
        {
            Debug.Log("E pressed");
            StartCoroutine(doorOpen.CloseDoor());

        }



    }
}







