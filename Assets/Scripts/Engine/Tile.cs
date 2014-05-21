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
    ArrayList adjacencyList;        //List of tiles that are touching this one (diagonals excluded)
    ArrayList diagonalAdjList;      //List of tiles that are touching this one (diagonals only)
    public GameObject objOnTile;    //GameObject that is currently on this tile
	private bool onStart = true;    //Checks if the player started on this tile
    private bool isWall = false;    //If the tile is a wall (unpathable)

	// Use this for initialization
	void Start () {
		adjacencyList = new ArrayList();
        diagonalAdjList = new ArrayList();
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

    public bool isOccupied()
    {
        if (objOnTile != null || isWall)
            return true;
        else
            return false;
    }

    public void addToAdjacencyList(GameObject tile)
    {
        adjacencyList.Add(tile);
    }

    public void addToDiagAdjList(GameObject tile)
    {
        diagonalAdjList.Add(tile);
    }

    public bool isValidMove(GameObject tile)
    {
        if (!tile.GetComponent<Tile>().isOccupied() && adjacencyList.Contains(tile) || diagonalAdjList.Contains(tile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
