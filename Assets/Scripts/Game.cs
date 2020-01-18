using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance = null;
    public List<Player> players;

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
            if (instance != null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    public Player Get_other_player(int team)
    {
        for (int i = 0; i < players.Count; ++i)
        {
            if (players[i].team != team)
            {
                return players[i];
            }
        }
        return null;
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }
}
