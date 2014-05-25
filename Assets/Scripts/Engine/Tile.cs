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
    public bool isExit = false;     //If the tile is the exit (takes player to next level)

    public struct tileLocation
    {
        public int rowNum;             //Row the tile is in
        public int colNum;             //Column the tile is in
    };
    private tileLocation location;

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

    public bool isValidMove(Tile tile)
    {
        if (!tile.isOccupied() && (adjacencyList.Contains(tile.gameObject) || diagonalAdjList.Contains(tile.gameObject)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isValidAttack(Tile tile)
    {
        if (adjacencyList.Contains(tile.gameObject) || diagonalAdjList.Contains(tile.gameObject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setLocation(int row, int col)
    {
        location.rowNum = row;
        location.colNum = col;
    }

    public tileLocation getLocation()
    {
        return location;
    }

    int distanceFromTile(Tile tile)
    {
        return (Mathf.Abs(tile.location.rowNum - location.rowNum + tile.location.colNum - location.colNum));
    }
}
