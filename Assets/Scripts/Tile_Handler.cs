using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles_Handler : MonoBehaviour
{
    public List<GameObject> tiles;
    private static Tiles_Handler instance;
    public static Tiles_Handler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Tiles_Handler>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Tile give_random()
    {
        return Instantiate(tiles[Random.Range(0, tiles.Count)]).GetComponent<Tile>();
    }
}
