﻿using System.Collections;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

    [SerializeField]
    bool ScreenShotActive = false;

    [SerializeField]
    KeyCode ActivateKey = KeyCode.Space;

    [SerializeField]
    string CaptureName = "ScreenShot.png";


	// Update is called once per frame
	private void Update () {
	    if(Input.GetKeyDown(ActivateKey) && ScreenShotActive)
        {
            Debug.Log("Screen Shot Captured.");
            ScreenCapture.CaptureScreenshot(CaptureName);
        }
	}
}
