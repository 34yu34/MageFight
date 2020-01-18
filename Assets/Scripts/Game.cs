using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Game : MonoBehaviour
{
    public static Game instance = null;
    public Player player1;
    public Player player2;
    public State state;

    public enum State
    {
        Fighting,
        Planning,
        Terrain,
        Buying
    }
    
    private Game()
    {
        state = State.Buying;
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
        }
        foreach (Character character in player2.characters)
        {
            character.Reset_round();
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
