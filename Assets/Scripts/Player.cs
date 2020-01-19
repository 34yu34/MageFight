using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public List<Character> characters;
    public int health;
    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        gold = 4;
        health = Game.Instance.health_settings;
    }

    public void Winner()
    {
        
    }

    public void Loser()
    {
        health--;

        if(health == 0)
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