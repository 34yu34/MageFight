using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public static Game instance = null;
    public Player player1;
    public Player player2;
    public int health_settings;
    public State state;
    public int gold_reward = 3;

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

    private void Start()
    {
        state = State.Buying;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {

        }
        if(state == State.Reset)
        {
            frameCount++;

            if(frameCount > 50)
            {
                Reset_characters();
                state = State.Buying;
                frameCount = 0;
                GetComponentInChildren<Buy_Menu>().round_reset();
            }
        }
        else if (state == State.Fighting)
        {
            int pl1 = 0;
            foreach (Character bob in player1.characters)
            {
                 if (bob.Is_alive() && bob.Is_on_board())
                {
                    pl1++;
                }
            }

            int pl2 = 0;
            foreach (Character bob in player2.characters)
            {
                if (bob.Is_alive() && bob.Is_on_board())
                {
                    pl2++;
                }
            }

            if (pl1 > 0 && pl2 == 0)
            {
                End_fighting(player1);
            }
            else if ( pl1 == 0 && pl2 > 0)
            {
                End_fighting(player2);
            }
            else if ( pl1 == 0 && pl2 == 0)
            {
                End_fighting(null);
            }
        }
    }
    private void End_fighting(Player pl)
    {
        state = Game.State.Reset;
        if(pl == null)
        {
            player1.Winner();
            player2.Winner();
        }
        else
        {
            Game.Instance.Get_other_player(pl).Loser();
            pl.Winner();
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
        player1.gold += gold_reward;
        foreach (Character character in player1.characters)
        {
            character.gameObject.SetActive(true);
            character.Reset_round();
            Snap_to_base(character);
        }
        player2.gold += gold_reward;
        foreach (Character character in player2.characters)
        {
            character.gameObject.SetActive(true);
            character.Reset_round();
            character.gameObject.SetActive(false);
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

    public Player Get_other_player(Player p)
    {
        return (p == player1) ? player2 : player1;
    }

    public List<Player> Get_players()
    {
        return new List<Player>{ player1, player2 };
    }

    public void End()
    {
        if (Global_Vars.loser == "Player1")
        {
            Global_Vars.loser = null;
            SceneManager.LoadScene("LossScene");
        }
        else if (Global_Vars.loser == "Player2")
        {
            Global_Vars.loser = null;
            SceneManager.LoadScene("WinScene");
        }
    }
}
