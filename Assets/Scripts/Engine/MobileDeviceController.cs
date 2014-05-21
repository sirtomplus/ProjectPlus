using UnityEngine;
using System.Collections;

public class MobileDeviceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //On my phone Pantech P9070 (Burst)
        //This is the button the is an arrow pointing up
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        //This is actually the button that looks like the back button
        if (Input.GetKeyDown("home"))
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        //My phone didn't have one of these
        if (Input.GetKeyDown("menu"))
        {
            Debug.Log("Menu button pressed");
        }
	}
}
