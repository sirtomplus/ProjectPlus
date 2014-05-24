using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour{
    public GUISkin mySkin;

	//use this for initialization
	void Start(){
	}

	//Update is called once per frame
	void Update(){
	}

    void OnGUI() {
        GUI.skin = mySkin;
        if(GUI.Button(new Rect(Screen.width - (Screen.width/5), Screen.height - (Screen.height/13), Screen.width/5, Screen.height/13), "Exit")){
            //This is actually killing the program and doesn't suspend it
            Application.Quit();
        }
    }
}