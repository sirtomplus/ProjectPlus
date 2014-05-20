using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public GameObject Tile;

	public Material[] Colors;
	public int numOfRows;
	public int numOfCols;
	ArrayList levelState;
	private GameObject Player;

	void Awake(){ 
		levelState = new ArrayList();
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {
		Tile.renderer.material = Colors[Random.Range(0, Colors.Length)];
		GenerateLevel(numOfRows,numOfCols);
		setPlayerStartingTile(levelState[(Random.Range (0, levelState.Count))] as GameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateLevel(int rows, int cols){
		for(int i = 0; i < rows; ++i){
			for(int j = 0; j < cols; ++j){
				GameObject tile = Instantiate(Tile, new Vector3(j, i, 0f), Quaternion.Euler(0f,0f,0f)) as GameObject;
				tile.renderer.material = Colors[Random.Range (0, Colors.Length)];
				levelState.Add(tile);
			}
		}
	}

	public void setPlayerStartingTile(GameObject Tile){
		Debug.Log (Tile.transform.position);
		Player.transform.position = Tile.transform.position;
		Player.GetComponent<Player>().setStartingTile(Tile);
	}
}
