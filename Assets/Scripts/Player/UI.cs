using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Script implementing GameOver UI for if the player dies or escapes


    public GameObject gameOver;
    public GameObject globalVol;
    public GameObject flashlight;
    public GameObject mothman;
    public GameObject player;
    public Player playerScript;
    public GameObject mainCamera;
    public MouseLook mouseLook;
    public GameObject endScreen;
    public float hValue = 5f;
    public CanvasGroup headphoneWarn;
    public GameObject headPhone;
    public bool timerFinished;

    public GameObject blood1;
    public GameObject blood2;
    public Animator bloodanim1;
    public Animator bloodanim2;
    
    private void Start()
    {
     
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerScript = player.GetComponent<Player>();
        mouseLook = mainCamera.GetComponent<MouseLook>();
        timerFinished = false;

        //blood effect
        blood1 = GameObject.FindWithTag("Blood1");
        blood2 = GameObject.FindWithTag("Blood2");
        bloodanim1 = blood1.GetComponent<Animator>();
        bloodanim2 = blood2.GetComponent<Animator>();
        bloodanim1.SetBool("Hit", false);
        bloodanim2.SetBool("Hit", false);

        StartCoroutine(HeadPhoneTimer());
    }
   private void Update()
   {
        if (hValue <= 0f)
        {
            headPhone.SetActive(false);
        }
        else if (timerFinished == true) 
        {
            hValue -= Time.deltaTime;
            headphoneWarn.alpha = hValue;
        }
   }
    public void OnGameOver()
   {
        //activate death UI
        playerScript.OnGameOver();
        playerScript.enabled = false;
        mouseLook.enabled = false;
        globalVol.SetActive(false);
        gameOver.SetActive(true);
        flashlight.SetActive(false);
        mothman.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void OnHit()
    {
        StartCoroutine(BloodTimer());
    }

    //retry button
  public void Retry()
  {
        SceneManager.LoadScene("Mothman");
  }


    // trigger activating win screen at end
    private void OnTriggerEnter(Collider other)
    {
            playerScript.enabled = false;
            mouseLook.enabled = false;
            endScreen.SetActive(true);
            flashlight.SetActive(false);
            mothman.SetActive(false);
            globalVol.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
    }



    IEnumerator HeadPhoneTimer()
    {
        yield return new WaitForSeconds(2);
        timerFinished = true;

    }
    IEnumerator BloodTimer()
    {
        bloodanim1.SetBool("Hit", true);
        bloodanim2.SetBool("Hit", true);
        yield return new WaitForSeconds(5f);
        bloodanim1.SetBool("Hit", false);
        bloodanim2.SetBool("Hit", false);

    }
}
