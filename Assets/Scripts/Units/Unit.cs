using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour{
    public int Health;
    public int Damage;                  //For debug purposes
    public GameObject curTile;
    protected bool isMoving = false;

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    public IEnumerator moveToTile(GameObject Tile)
    {
        isMoving = true;
        curTile.GetComponent<Tile>().LeaveTile();
        curTile = Tile;
        Tile.GetComponent<Tile>().OccupyTile(this.gameObject);
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, Tile.transform.position, .1f);
            if (Mathf.Abs(transform.position.x - Tile.transform.position.x) < .09f &&
                                   Mathf.Abs(transform.position.y - Tile.transform.position.y) < .09f)
            {
                transform.position = Tile.transform.position;

                break;
            }
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
    }

    public void setStartingTile(GameObject Tile)
    {
        curTile = Tile;
        Tile.GetComponent<Tile>().OccupyTile(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Attack(GameObject unit)
    {
        unit.GetComponent<Unit>().TakeDamage(Damage);
    }
}