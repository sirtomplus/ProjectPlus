using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private GameObject Player;
	private float targetX;
	private float targetY;

	void Awake(){
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

	}

	void LateUpdate(){
		targetX = Mathf.Lerp (transform.position.x, Player.transform.position.x, Time.deltaTime);
		targetY = Mathf.Lerp (transform.position.y, Player.transform.position.y, Time.deltaTime);
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
