using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {

    }
}