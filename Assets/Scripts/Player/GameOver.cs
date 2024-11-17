using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject globalVol;
    public GameObject flashlight;
    public GameObject mothman;

   public void OnGameOver()
   {
        globalVol.SetActive(false);
        gameOver.SetActive(true);
        flashlight.SetActive(false);
        mothman.SetActive(false);
   }


}
