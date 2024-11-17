using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject globalVol;
    public GameObject flashlight;
    public GameObject mothman;

    private void Start()
    {
        Cursor.visible = false;
    }
    public void OnGameOver()
   {
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
}
