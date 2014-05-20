using UnityEngine;
using System.Collections;

/*AdjacencyList
 * 0    Up
 * 1    UpRight
 * 2    Right
 * 3    DownRight
 * 4    Down
 * 5    DownLeft
 * 6    Left
 * 7    UpLeft
 */

public class Tile : MonoBehaviour {
    ArrayList adjacencyList;
    public GameObject objOnTile;
	private bool onStart = true;

	// Use this for initialization
	void Start () {
		adjacencyList = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2")){
			OutputAdjList();
		}
	}

	void LateUpdate(){
		onStart = false;
	}

	void OnTriggerEnter2D(Collider2D col){
//		if(col.tag == "Tile"){
//			adjacencyList.Add(col.gameObject);
//		}
		if(col.tag == "Player" && onStart){
			Debug.Log ("Found player");
			col.gameObject.GetComponent<Player>().setStartingTile(this.gameObject);
		}
	}

	void OutputAdjList(){
		for(int i = 0; i < adjacencyList.Count; ++i){
			Debug.Log (adjacencyList[i]);
		}
	}

	public void OccupyTile(GameObject obj){
		objOnTile = obj;
	}

	public void LeaveTile(){
		objOnTile = null;
	}
}
