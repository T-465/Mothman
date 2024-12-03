using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 _orignalPosOfCam;
    public float shakeFrequency;
    public bool shaking;


    void Start()
    {
      _orignalPosOfCam = cameraTransform.position;
    }

    private void Update()
    {
        if (!shaking)
        {
            //tracks the camera position to reset after shaking
            _orignalPosOfCam = cameraTransform.position;
            StopShake();
        }
        if (shaking)
        {
            CameraShaker();
           
        }
    }

    public void CameraShaker()
    {
        cameraTransform.position = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
    }

    public void StopShake()
    {
      shaking = false;
      //Return the camera to it's original position.
      cameraTransform.position = _orignalPosOfCam;
    }
    
}
