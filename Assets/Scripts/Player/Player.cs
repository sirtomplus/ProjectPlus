using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{
	public int Health;

	void Start() {
	}

	void Update(){
        if (Input.GetMouseButtonUp(0))
        {
			Debug.Log ("Mouse clicked");
            //Mouse click to simulate touch controls
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
			if(hit.collider != null){
				Debug.Log ("Hit something");
				if(hit.collider.tag == "Tile"){
                	Debug.Log("Hit Tile");
					Debug.Log (Time.deltaTime);
					StartCoroutine(moveToTile(hit.collider.gameObject));
				}

            }
		}
	}

	IEnumerator moveToTile(GameObject Tile){
		while(true){
			transform.position = Vector3.Lerp (transform.position, Tile.transform.position, .08f);
			if(Mathf.Abs(transform.position.x - Tile.transform.position.x) < .02f &&
			                       Mathf.Abs(transform.position.y - Tile.transform.position.y) < .02f){
				transform.position = Tile.transform.position;
				Debug.Log ("Got there");
				break;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}