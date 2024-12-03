using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIGameEnds : MonoBehaviour
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


    private void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerScript = player.GetComponent<Player>();
        mouseLook = mainCamera.GetComponent<MouseLook>();
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
}
