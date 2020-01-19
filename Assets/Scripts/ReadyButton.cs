using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseUp);
    }

    private void OnMouseUp()
    {
        if (Game.Instance.state == Game.State.Planning)
        {
            Game.Instance.state = Game.State.Fighting;
        }
        else if (Game.Instance.state == Game.State.Buying)
        {
            if (Game.Instance.player1.terrainNum > 0)
            {
                Game.Instance.state = Game.State.Terrain;
            }
            else
            {
                Game.Instance.state = Game.State.Planning;
            }
        }
        else if (Game.Instance.state == Game.State.Terrain)
        {
            Game.Instance.state = Game.State.Planning;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
