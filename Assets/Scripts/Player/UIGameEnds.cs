using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIGameEnds : MonoBehaviour
{
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

  public void Retry()
  {
        SceneManager.LoadScene("Mothman");
  }


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
