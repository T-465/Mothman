using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
        public Transform cameraTransform;
        public Vector3 _orignalPosOfCam;
        public float shakeFrequency;
    public bool shaking;

        // Start is called before the first frame update
        void Start()
        {
            //When the game starts make sure to assign the origianl possition of the camera, to its current
            //position, supposedly it is where you want the camera to return after shaking.
            _orignalPosOfCam = cameraTransform.position;
        }

    // Update is called once per frame
    private void Update()
    {
        if (!shaking)
        {
            _orignalPosOfCam = cameraTransform.position;
        }
       
    }

    public void CameraShaker()
        {
        shaking = true;
            cameraTransform.position = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
        }

        public void StopShake()
        {
           shaking = false;
            //Return the camera to it's original position.
            cameraTransform.position = _orignalPosOfCam;
        }
    
}
