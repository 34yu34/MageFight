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
            Game.Instance.state = Game.State.Terrain;
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
