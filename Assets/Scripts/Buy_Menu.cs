using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character_Enum;

public class Buy_Menu : MonoBehaviour
{
    public GameObject Air_Mage_Prefab;
    public GameObject Fire_Mage_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On_Buy_Button(int Type)
    {
        Character_Type Char_Type = (Character_Type)Type;

        GameObject[] GameObject_tiles = GameObject.FindGameObjectsWithTag("Tile");
        List<Tile> empty_tiles = new List<Tile>();
        foreach (GameObject GameObject_tile in GameObject_tiles)
        {
            Tile tile = GameObject_tile.GetComponent<Tile>();
            if (tile != null)
            {
                if (tile.Is_available() && !tile.is_playable)
                    empty_tiles.Add(GameObject_tile.GetComponent<Tile>());
            }
        }
        
        if (empty_tiles.Count > 0)
        {
            Vector3 spawn_position = empty_tiles[0].transform.position + (new Vector3(0,3,0));
            GameObject new_mage = null;
            switch (Char_Type)
            {
                case Character_Type.Air:
                    new_mage = Instantiate(Air_Mage_Prefab, spawn_position, Quaternion.identity);
                    break;
                case Character_Type.Fire:
                    new_mage = Instantiate(Fire_Mage_Prefab, spawn_position, Quaternion.identity);
                    break;
            }
            Character mage = new_mage.GetComponent<Character>();
            empty_tiles[0].Occupy(ref mage);
            mage.tile = empty_tiles[0];
        }
    }
}
