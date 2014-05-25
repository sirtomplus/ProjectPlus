using UnityEngine;
using System.Collections;


public class CameraZoom : MonoBehaviour{
    public float orthoZoomSpeed = 0.5f;

	void Start(){

	}

	//Update is called once per frame
	void Update(){
        if (Input.touchCount == 2) {
            //Store both touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //Find the magnitude of the vector (the distance) between the touches in each frame
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //Find the difference in the distance between each frame
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            //Make sure the orthographic size never drops below zero
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
        }
	}

	void FixedUpdate(){

	}
}