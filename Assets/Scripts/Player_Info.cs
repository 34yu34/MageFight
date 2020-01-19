using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Info : MonoBehaviour
{
    public bool isMine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] lines = gameObject.GetComponent<Text>().text.Split(new[] { '\r', '\n' });
        lines[0] += '\n';
        lines[1] = (isMine) ? ("Health: " + Game.Instance.player1.health + '\n') : ("Health: " + Game.Instance.player2.health + '\n');
        lines[2] = (isMine) ? ("Gold: " + Game.Instance.player1.gold + '\n') : ("Gold: " + Game.Instance.player2.gold + '\n');
        gameObject.GetComponent<Text>().text = string.Concat(lines);
    }
}
