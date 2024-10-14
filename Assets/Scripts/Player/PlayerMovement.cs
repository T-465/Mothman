using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerHealth = 3;
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

    #region Flashlight Variables
    public GameObject volumebeam;
    public Light FlashLight;
    public Animator batteryanimator;
    public float countdown = 10;
    public bool flashlightfull;
    public bool flashlightdead;
    public bool flashlight2bar;
    public bool flashlight1bar;
    #endregion

    #region Door
    //public DoorOpen doorOpen;

    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Start()
    {
        flashlightfull = true;
        batteryanimator.SetBool("BatteryFull", true);
    }

    private void Update()
    {
        #region PlayerMove
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
        #endregion

        /* if (Input.GetKeyDown(KeyCode.E) && doorOpen.doorclosed == true)
         {
             Debug.Log("E pressed");
             StartCoroutine(doorOpen.OpenDoor());
         }
         else if (Input.GetKeyDown(KeyCode.E) && doorOpen.dooropen == true)
         {
             Debug.Log("E pressed");
             StartCoroutine(doorOpen.CloseDoor());

         }
        */



        #region Flashlight
        // Check for player input and toggle the flashlight...
        if (countdown >= 10)
        {
            countdown = 10;

        }
        if (Input.GetMouseButton(0))
        {
              
            ToggleFlashlight();
        }
        if (FlashLight.enabled == true)
        {
            countdown -= Time.deltaTime;
            volumebeam.SetActive(true);
        }
        else if (FlashLight.enabled == false) 
        {
            volumebeam.SetActive(false);
            StartCoroutine(Cooldown());
            IEnumerator Cooldown()
            {
                yield return new WaitForSeconds(4);
                countdown += Time.deltaTime;
            }
        }
        if (countdown >= 9)
        {
            flashlight2bar = false;
            flashlightfull = true;
            
            batteryanimator.SetBool("BatteryFull", true);
            batteryanimator.SetBool("Battery2Bar", false);
         
        }

        if (countdown <= 7 && countdown >= 4)
        {
            flashlight2bar = true;
            flashlightfull = false;
            flashlight1bar = false;
            batteryanimator.SetBool("BatteryFull", false);
            batteryanimator.SetBool("Battery2Bar", true);
            batteryanimator.SetBool("Battery1Bar", false);
        }
        if (countdown <= 4 && countdown >= 0)
        {
            flashlight1bar = true;
            flashlight2bar = false;
            flashlightdead = false;
            batteryanimator.SetBool("Battery2Bar", false);
            batteryanimator.SetBool("Battery1Bar", true);
            batteryanimator.SetBool("BatteryEmpty", false);
        }
        if (countdown <= 0)
        {
            volumebeam.SetActive(false);
            FlashLight.enabled = false;
            flashlightdead = true;
            flashlight1bar = false;
            flashlight2bar = false;
            batteryanimator.SetBool("Battery1Bar", false);
            batteryanimator.SetBool("BatteryEmpty", true);
            batteryanimator.SetBool("Battery2Bar", false);
        }
        #endregion

    }
    private void ToggleFlashlight()
    {

        FlashLight.enabled = !FlashLight.enabled;
    }
}







