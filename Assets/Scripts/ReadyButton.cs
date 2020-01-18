using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }

    private void OnMouseUp()
    {
        if (Game.Instance.state == Game.State.Planning)
        {
            Game.Instance.state = Game.State.Fighting;
        }
        else if (Game.Instance.state == Game.State.Buying)
        {
            Game.Instance.state = Game.State.Planning;
            Game.Instance.Reset_characters();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
