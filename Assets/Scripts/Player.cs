using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public List<Character> characters;
    public uint health;
    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        gold = 4;
        health = Global_Vars.health_setting;
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
            Global_Vars.loser = gameObject.GetComponent<Player>().name;
            Game.Instance.End();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}