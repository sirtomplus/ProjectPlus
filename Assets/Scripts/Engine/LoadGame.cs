using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour{

	//use this for initialization
	void Start(){
        StartCoroutine(loadLevel());
	}

	//Update is called once per frame
	void Update(){
	}

    IEnumerator loadLevel() {
        yield return new WaitForEndOfFrame();
        Application.LoadLevel("TestScene");
    }
}