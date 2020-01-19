using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Info_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Selection_Handler.Instance.get_selection_text() != null)
        {
            GetComponentInParent<Canvas>().enabled = true;
            GetComponentInChildren<Text>().text = Selection_Handler.Instance.get_selection_text();
            GetComponentsInChildren<Image>()[1].sprite = Selection_Handler.Instance.get_selection_sprite();
        }
        else
        {
            GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
