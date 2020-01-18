using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Mouse_Hover : MonoBehaviour
{
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
    }

    private void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        rend.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
