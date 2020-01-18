using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Game : MonoBehaviour
{
    public static Game instance = null;
    public Player player1;
    public Player player2;
    public int health_settings;
    public State state;

    private int frameCount;

    public enum State
    {
        Fighting,
        Planning,
        Terrain,
        Reset,
        Buying
    }
    
    private Game()
    {
        state = State.Buying;
        frameCount = 0;
    }

    private void Update()
    {
        if(state == State.Reset)
        {
            frameCount++;

            if(frameCount > 50)
            {
                Reset_characters();
                state = State.Buying;
                frameCount = 0;
            }
        }
    }

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Game>();
            }
            return instance;
        }
    }

    public void Reset_characters()
    {
        foreach (Character character in player1.characters)
        {
            character.Reset_round();
            Snap_to_base(character);
        }
        foreach (Character character in player2.characters)
        {
            character.Reset_round();
        }
    }

    private void Snap_to_base(Character character)
    {
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
            Vector3 spawn_position = empty_tiles[0].transform.position + (new Vector3(0, 3, 0));
            character.transform.position = spawn_position;
            empty_tiles[0].Occupy(ref character);
        }
    }

    public Player Get_other_player(int team)
    {
        return (team == 0) ? player2 : player1;
    }

    public Player Get_player(int team)
    {
        return (team == 0) ? player1 : player2;
    }

    public List<Player> Get_players()
    {
        return new List<Player>{ player1, player2 };
    }
}
