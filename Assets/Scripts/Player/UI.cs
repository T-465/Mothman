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
    public float aValue = 5f;
    public CanvasGroup headphoneWarn;
    public GameObject headPhone;
    public bool timerFinished;
    private void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerScript = player.GetComponent<Player>();
        mouseLook = mainCamera.GetComponent<MouseLook>();
        timerFinished = false;

        StartCoroutine(HeadPhoneTimer());
    }
   private void Update()
   {
        if (aValue <= 0f)
        {
            headPhone.SetActive(false);
        }
        else if (timerFinished == true) 
        {
            aValue -= Time.deltaTime;
            headphoneWarn.alpha = aValue;
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
    }
    IEnumerator HeadPhoneTimer()
    {
        yield return new WaitForSeconds(2);
        timerFinished = true;

    }
}
