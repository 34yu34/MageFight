using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Game.Instance.state)
        {
            case Game.State.Buying:
                gameObject.GetComponent<Text>().text = "Buying Phase";
                break;
            case Game.State.Terrain:
                gameObject.GetComponent<Text>().text = "Terrain Phase";
                break;
            case Game.State.Planning:
                gameObject.GetComponent<Text>().text = "Placement Phase";
                break;
            case Game.State.Fighting:
                gameObject.GetComponent<Text>().text = "Fight!";
                break;
        }
    }
}
