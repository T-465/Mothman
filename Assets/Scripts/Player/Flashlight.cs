using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Flashlight : MonoBehaviour, ISwitchable
{
    public PlayerInput playerinput;
    public GameObject volumebeam;
    public Light FlashLight;
    public Animator batteryanimator;
    public float countdown = 10;
    public bool flashlightfull;
    public bool flashlightdead;
    public bool flashlight2bar;
    public bool flashlight1bar;

    private void Start()
    {
        flashlightfull = true;
        batteryanimator.SetBool("BatteryFull", true);
    }

    public void Update()
    {


        #region Flashlight
        // Check for player input and toggle the flashlight
        if (countdown >= 10)
        {
            countdown = 10;

        }
        if (Input.GetMouseButton(0))
        {
            Toggle();
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
    public void Toggle()
    {
 
      FlashLight.enabled = !FlashLight.enabled;
    }
}
