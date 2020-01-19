using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public List<Character> characters;
    public int player_health;
    public int player_gold;

    // Start is called before the first frame update
    void Start()
    {
        player_gold = 3;
        player_health = Game.Instance.health_settings;
    }

    public void Winner()
    {
        
    }

    public void Loser()
    {
        player_health--;

        if(player_health == 0)
        {
            //show loss for self and win for other
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}