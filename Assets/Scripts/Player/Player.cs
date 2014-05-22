using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{
	public int Health;
	GameObject curTile;
	GameObject nextTile;
	private bool isMoving = false;

	void Awake(){
	}

	void Start() {
	}

	void Update(){
        if (Input.GetMouseButtonUp(0))
        {
            //Mouse click to simulate touch controls
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
			if(hit.collider != null){
				if(hit.collider.tag == "Tile" && !isMoving){
                    if(curTile.GetComponent<Tile>().isValidMove(hit.collider.gameObject.GetComponent<Tile>()))
					    StartCoroutine(moveToTile(hit.collider.gameObject));
				}
				else if(hit.collider.tag == "Tile"){
					nextTile = hit.collider.gameObject;
				}
            }
		}
	}

	void FixedUpdate(){
		if(nextTile != null && !isMoving){
            if (curTile.GetComponent<Tile>().isValidMove(nextTile.GetComponent<Tile>()))
			    StartCoroutine(moveToTile(nextTile));
			nextTile = null;
		}
	}

	IEnumerator moveToTile(GameObject Tile){
		isMoving = true;
		curTile.GetComponent<Tile>().LeaveTile();
		curTile = Tile;
		Tile.GetComponent<Tile>().OccupyTile(this.gameObject);
		while(true){
			transform.position = Vector3.Lerp (transform.position, Tile.transform.position, .1f);
			if(Mathf.Abs(transform.position.x - Tile.transform.position.x) < .09f &&
			                       Mathf.Abs(transform.position.y - Tile.transform.position.y) < .09f){
				transform.position = Tile.transform.position;

				break;
			}
			yield return new WaitForFixedUpdate();
		}
		isMoving = false;
	}

	public void setStartingTile(GameObject Tile){
		curTile = Tile;
		Tile.GetComponent<Tile>().OccupyTile(this.gameObject);
	}
}