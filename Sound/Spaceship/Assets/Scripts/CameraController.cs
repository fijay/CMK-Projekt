using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script could be used to set the device camera on a plane.

public class CameraController : MonoBehaviour {

    public WebCamTexture webcamTexture;
    public Quaternion baseRotation;

    // Use this for initialization
    void Start () {
        webcamTexture = new WebCamTexture();

        float ScreenWidth = Screen.width;
        float ScreenHeight = Screen.height;
        transform.localScale = new Vector3(ScreenWidth/1000, 1, ScreenHeight/1000);

        baseRotation = transform.rotation;

        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = baseRotation * Quaternion.AngleAxis(webcamTexture.videoRotationAngle, Vector3.up);
    }
}
