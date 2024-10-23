using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MothmanAI : MonoBehaviour
{
    //Mothmans Nav Mesh Agent component
    public NavMeshAgent ai;

    //Mothmans Mesh Renderer component
    public SkinnedMeshRenderer mothMesh;

    //Slender's movement speed
    public float m_speed;

    
    //Distance from which Mothman catches the player if they get too close
    public float catchDistance;

    //The player's health
    public float playerHealth = 100;

    //The player's Transform
    public Transform player;

    //Slender's main Transform
    public Transform mothMainTransform;

    //Mothmans jumpscare camera
    public GameObject jumpscareCam;
    
    //Black image that appears after death
    public GameObject blackscreen;

    //Mothmans destination
    Vector3 dest;

    //Integer used to randomize what position Mothman will teleport to next
    int randNum, randNum2;

    //Integer used to randomize the chance of teleporting after the player looks away
    int teleportChance; 

    //Integers used to make sure things don't occur more than once in the Update() void
    int token, token3, token4;


    //Enable this if you want your Cursor to be enabled after death
    public bool enableCursorAfterDeath;
    
    //The name of the scene that will be loaded 
    public string scenename;

    //Mothmans distance from the player
    float aiDistance;

    //Float used for the static image's opacity amount
    float staticAmount;

    //Float used for the static sound's volume
    float staticVolume;

    //List of Mothmans teleport destinations
    public List<Transform> teleportDestinations;

    //Mothmans raycast script
    public raycastMoth raycastScript;


    //The sound that plays when looking
    public AudioSource buzzSound;


    //The player's Camera
    public Camera playerCam;

    //The Start() void makes stuff happen at the start of the scene or when an object with the attached script is set active
    void Start()
    {
        AudioListener.pause = false; //Game's Audio Listener will not be paused  
    }
    
    //The resetSlender() void gives the chance to teleport upon the player moving out of his sight or looking away
    void resetMoth()
    {
        teleportChance = Random.Range(0, 2); //teleportChance will equal to a random range of numbers between 0 and 2 (technically 0 and 1, since the last is never picked)
        if (teleportChance == 0) //If teleportChance equals to 0, Mothman will teleport to a random destination
        {
            randNum = Random.Range(0, teleportDestinations.Count); //randNum will equal to a random range of numbers between 0 and the amount of transforms in the teleportDestinations list
            mothMainTransform.position = teleportDestinations[randNum].position; //Slender will teleport to random destination determined by randNum
        }
    }

    //Coroutine for when the player is killed
    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(3.5f); //After 3.5 seconds,
        blackscreen.SetActive(true); //The black screen will be set active
        AudioListener.pause = true; //The game's Audio Listener wil be paused so there is no more sound
        yield return new WaitForSeconds(6f); //After 6 seconds,
        if(enableCursorAfterDeath == true) //If the enableCursorAfterDeath bool equals to true,
        {
            Cursor.visible = true; //The Cursor will be turned on in case you're loading into a scene where you'd like to use your mouse (like the main menu)
            Cursor.lockState = CursorLockMode.None; //The Cursor will be unlocked in case you're loading into a scene where you'd like to use your mouse (like the main menu)
        }   
        SceneManager.LoadScene(scenename); //The scene determined by the scenename string will be loaded
    }

    //The Update() void makes things happen every frame
    void Update()
    {
        // Get the player's camera's frustum planes (the camera's view)
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        //If Slender is within the player's camera's FOV and the player's health is over 0,
        if (GeometryUtility.TestPlanesAABB(planes, mothMesh.bounds) && playerHealth > 0)
        {
            ai.enabled = false; //Slender's Nav Mesh Agent component will be disabled
            ai.speed = 0; //Slender's speed will equal to 0
            ai.SetDestination(transform.position); //Sets the AI's destination to itself.
            if (raycastScript.detected == true)
            {
                token3 = 0; //token3 = 0, which means the function tied to that token can happen again
                if (token4 == 0) //If token4 equals to 0,
                {
                    randNum2 = Random.Range(0, 2); //randNum2 will equal to a random range of numbers between 0 and 2
                    if (randNum2 == 0) //If randNum2 equals to 0,
                    {
                        
                    }
                    token4 = 1; //token4 = 1, which means this function won't occur again until token4 = 0
                }
                mothMainTransform.position = mothMainTransform.position; //Slender's position will equal to his own position

                if (staticVolume > 1) //If staticVolume is over 1,
                {
                    staticVolume = 1; //staticVolume equals to 1
                }
                if (staticAmount > 0.9f) //If staticAmount is over 0.9,
                {
                    staticAmount = 0.9f; //staticAmount equals to 0.9
                }
            }
        }
        //If Slender is out of the player's camera's FOV and the player's health is over 0 OR if the raycast isn't hitting the player and the player's health is over 0,
        if (!GeometryUtility.TestPlanesAABB(planes, mothMesh.bounds) && playerHealth > 0 || raycastScript.detected == false && playerHealth > 0)
        {
            ai.speed = m_speed; //Slender's speed will equal to the value of m_speed
            ai.enabled = true; //Slender's Nav Mesh Agent component will be enabled
            if (token3 == 0) //If token3 equals to 0,
            {
                resetMoth(); //The resetSlender() void will occur
                token3 = 1; //token3 = 1, which means this function won't occur again until token3 = 0
            }
            dest = player.position; //dest will equal to the player's position
            token4 = 0; //token4 = 0, which means the function tied to that token can happen again
            ai.destination = dest; //Slender's destination will equal to dest

            if (staticVolume < 0) //If staticVolume is less than 0,
            {
                staticVolume = 0; //staticVolume equals to 0
            }
            if (staticAmount < 0) //If staticAmount is less than 0,
            {
                staticAmount = 0; //staticAmount equals to 0
            }
            if (playerHealth > 100) //If playerHealth is over 100,
            {
                playerHealth = 100; //playerHealth equals to 100
            }
        }

   
        buzzSound.volume = staticVolume; //The static sound's Audio Source's volume equals to the value of staticVolume


        this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z)); //Slender will always be looking in the player's direction

        aiDistance = Vector3.Distance(this.transform.position, player.position); //aiDistance equals to the distance between Slender and the player
        Debug.Log(aiDistance); //Debug Log which prints out Slender's distance from the player

        if (playerHealth <= 0) //If playerHealth is less than or equal to 0,
        {
            StartCoroutine(killPlayer()); //The killPlayer() coroutine will start

            if (staticVolume > 1) //If staticVolume is over 1,
            {
                staticVolume = 1; //staticVolume equals to 1
            }
            if (staticAmount > 0.9f) //If staticAmount is over 0.9,
            {
                staticAmount = 0.9f; //staticAmount equals to 0.9
            }
            player.gameObject.SetActive(false); //The player object will be disabled
            jumpscareCam.SetActive(true); //Slender's jumpscare camera will be enabled
            ai.speed = 0; //Slender's speed will equal to 0
            ai.SetDestination(transform.position); //Sets the AI's destination to itself.
        }
        if (aiDistance <= catchDistance) //If Slender's distance from the player is less than or equal to catchDistance,
        {
            if(token == 0) //If token equals to 0,
            {
                playerHealth = 0; //playerHealth equals to 0
                token = 1; //token = 1, which means this function won't occur again until token = 0
                //The player is killed 
            }
        }
    }
}
