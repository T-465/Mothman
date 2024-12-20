using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleCollisions : MonoBehaviour
{
    // Script that activates/deactivates teleport areas around the map as the player progresses

    public GameObject telespots;
    public GameObject mothMan;
    public GameObject player;

    public GameObject tele1;
    public GameObject tele2;
    public GameObject tele3;
    public GameObject tele4;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mothMan.SetActive(false);
        telespots.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        //Player enter teleport areas
            Debug.Log("telespots");
            telespots.SetActive(true);

            tele1 = GameObject.FindWithTag("Tele1");
            tele2 = GameObject.FindWithTag("Tele2");
            tele3 = GameObject.FindWithTag("Tele3");
            tele4 = GameObject.FindWithTag("Tele4");
   
        mothMan.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        //Player in safezone 

        tele1 = null;
        tele2 = null;
        tele3 = null;
        tele4 = null;
        telespots.SetActive(false);

        mothMan.SetActive(false);
    }


}
