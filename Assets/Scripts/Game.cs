using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance = null;
    public Player player1;
    public Player player2;

    public State state;

    public enum State
    {
        Fighting,
        Menu,
        Buying
    }
    
    private Game()
    {
        state = State.Menu;
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

    public Player Get_other_player(int team)
    {
        return (team == 0) ? player2 : player1;
    }
}
