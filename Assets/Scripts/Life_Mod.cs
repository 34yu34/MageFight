using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Mod : MonoBehaviour
{
    public bool isUp;
    public bool isDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseUp()
    {
        var current = int.Parse(this.transform.parent.GetComponent<TextMesh>().text);
        
        if (isUp)
        {
            current += 1;
        }

        if (isDown)
        {
            current -= 1;
        }

        this.transform.parent.GetComponent<TextMesh>().text = current.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
