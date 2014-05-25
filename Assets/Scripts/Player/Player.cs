using UnityEngine;
using System.Collections;

public class Player : Unit{
	GameObject nextTile;

	void Awake(){
	}

	void Start() {
        DontDestroyOnLoad(gameObject);
	}

	void Update(){
        if (curTile != null && !isMoving && curTile.GetComponent<Tile>().isExit) {
            curTile = null;
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Mouse click to simulate touch controls
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
			if(hit.collider != null){
                Debug.Log(hit.collider.tag);
				if(hit.collider.tag == "Tile" && !isMoving){
                    if (curTile.GetComponent<Tile>().isValidMove(hit.collider.gameObject.GetComponent<Tile>()))
                        StartCoroutine(moveToTile(hit.collider.gameObject));
                    else if (curTile.GetComponent<Tile>().isOccupied() && curTile.GetComponent<Tile>().isValidAttack(hit.collider.gameObject.GetComponent<Tile>())) {
                        Attack(hit.collider.gameObject.GetComponent<Tile>().objOnTile);
                    }
				}
				else if(hit.collider.tag == "Tile"){
					nextTile = hit.collider.gameObject;
				}
                if (hit.collider.tag == "Enemy" && !isMoving) {
                    if (curTile.GetComponent<Tile>().isValidAttack(hit.collider.gameObject.GetComponent<Enemy>().curTile.GetComponent<Tile>())) {
                        Attack(hit.collider.gameObject);
                    }
                }
            }
		}
        if (nextTile != null && !isMoving)
        {
            if (curTile.GetComponent<Tile>().isValidMove(nextTile.GetComponent<Tile>()))
                StartCoroutine(moveToTile(nextTile));
            nextTile = null;
        }
	}

	void FixedUpdate(){
		
	}
}