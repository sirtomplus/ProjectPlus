using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public GameObject Tile;

	public Material[] Colors;
	public int numOfRows;
	public int numOfCols;
    //public GameObject[] levelState;
    public GameObject[][] levelState;
	private GameObject Player;          //Maybe cache the player object
    private GameObject Enemy;           //For Debugging purposes

	void Awake(){ 
        //levelState = new GameObject[numOfCols*numOfRows];

        //Loads the prefab Enemy at runtime and instantiates it
        Enemy = Instantiate(Resources.Load<Object>("Prefabs/Enemy"), new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;

        levelState = new GameObject[numOfRows][];
        for (int i = 0; i < numOfRows; ++i)
        {
            levelState[i] = new GameObject[numOfCols];
        }
        Player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {
		Tile.renderer.material = Colors[Random.Range(0, Colors.Length)];
		GenerateLevel();
        int startingTileRow = Random.Range(0, numOfRows);
        int startingTileCol = Random.Range(0, numOfCols);
        while (levelState[startingTileRow][startingTileCol].GetComponent<Tile>().isOccupied())
        {
            startingTileRow = Random.Range(0, numOfRows);
            startingTileCol = Random.Range(0, numOfCols);
        }
        setPlayerStartingTile(levelState[startingTileRow][startingTileCol]);
        while (levelState[startingTileRow][startingTileCol].GetComponent<Tile>().isOccupied())
        {
            startingTileRow = Random.Range(0, numOfRows);
            startingTileCol = Random.Range(0, numOfCols);
        }
        setEnemyStartingTile(levelState[startingTileRow][startingTileCol]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateLevel(){
		for(int i = 0; i < numOfRows; ++i){
			for(int j = 0; j < numOfCols; ++j){
				GameObject tile = Instantiate(Tile, new Vector3(j, i, 0f), Quaternion.Euler(0f,0f,0f)) as GameObject;
                //tile.renderer.material = Colors[Random.Range (0, Colors.Length)];
                //string path = "Materials/Color" + Random.Range(0, 2);
                tile.renderer.material = Resources.Load<Material>("Materials/Color" + Random.Range(0, 2));
                //levelState[i*8+j] = tile;
                levelState[i][j] = tile;
			}
		}
        //Coroutine to wait until the next frame because the ArrayList in the Tile objects have not been initialized yet
        StartCoroutine(WaitBeforeSettingAdjList());
        ChooseExit(levelState[Random.Range(0, numOfRows)][Random.Range(0, numOfCols)].GetComponent<Tile>());
	}

    void SetAdjacencyList()
    {
        for (int i = 0; i < numOfRows; ++i)
        {
            for (int j = 0; j < numOfCols; ++j)
            {
                if (j == 0 && i == 7)
                {
                    //If the tile is in the top left hand corner
                    //Add the tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                    //Add the tile on the bottom right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j + 1]);
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                }
                else if (j == 7 && i == 7)
                {
                    //If the tile is on the top right hand corner
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the bottom left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j - 1]);
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                }
                else if (j == 0 && i == 0)
                {               
                    //If the tile is on the bottom left hand corner
                    //Add the tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                    //Add the tile on the top right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j + 1]);
                    //Add the tile on the top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                }
                else if (j == 7 && i == 0) 
                {
                    //If the tile is on the bottom right hand corner
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the topleft
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j - 1]);
                    //Add the tile on the top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                }
                else if (i == 7)
                {
                    //If the tile is on the top row but not on a corner
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the bottom left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j - 1]);
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                    //Add the tile on the bottom right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j + 1]);
                    //Addt he tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                }
                else if (i == 0)
                {
                    //If the tile is on the bottom row but not a corner
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the top left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j - 1]);
                    //Add the tile on the top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                    //Add the tile on the top right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j + 1]);
                    //Add the tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                }
                else if (j == 0)
                {
                    //If the tile is on the left column but not a corner
                    //Add the tile on the top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                    //Add the tile on the top right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j + 1]);
                    //Add the tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                    //Add the tile on the bottom right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j + 1]);
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                }
                else if (j == 7)
                {
                    //If the tile is on the right column but not a corner
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                    //Add the tile on the bottom left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j - 1]);
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the top left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j - 1]);
                    //Add the tile on the top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                }
                else
                {
                    //If the tile is not on the edges of the level (adjacent to 8 tiles)
                    //Add the tile ont he top
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i + 1][j]);
                    //Add the tile on the top right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j + 1]);
                    //Add the tile on the right
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j + 1]);
                    //Add the tile on the bottom right
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j + 1]);
                    //Add the tile on the bottom
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i - 1][j]);
                    //Add the tile on the bottom left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i - 1][j - 1]);
                    //Add the tile on the left
                    levelState[i][j].GetComponent<Tile>().addToAdjacencyList(levelState[i][j - 1]);
                    //Add the tile on the top left
                    levelState[i][j].GetComponent<Tile>().addToDiagAdjList(levelState[i + 1][j - 1]);
                }
            }
        }
    }

    IEnumerator WaitBeforeSettingAdjList()
    {
        yield return new WaitForFixedUpdate();
        SetAdjacencyList();
    }

	public void setPlayerStartingTile(GameObject Tile){
		Player.transform.position = Tile.transform.position;
		Player.GetComponent<Player>().setStartingTile(Tile);
	}

    public void setEnemyStartingTile(GameObject Tile)
    {
        Enemy.transform.position = Tile.transform.position;
        Enemy.GetComponent<Unit>().setStartingTile(Tile);
    }

    void ChooseExit(Tile tile) {
        tile.renderer.material.color = Color.red;
        tile.isExit = true;
    }
}
