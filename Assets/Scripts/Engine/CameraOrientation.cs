using UnityEngine;
using System.Collections;

public class CameraOrientation : MonoBehaviour{

	//use this for initialization
	void Start(){
        Screen.orientation = ScreenOrientation.AutoRotation;
	}

	//Update is called once per frame
	void Update(){

	}

    void FixedUpdate() {
        if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
            Camera.main.orthographicSize = 4;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
            Camera.main.orthographicSize = 3;
        }
    }
}