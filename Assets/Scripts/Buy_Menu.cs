using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character_Enum;

public class Buy_Menu : MonoBehaviour
{
    public GameObject Air_Mage_Prefab;
    public GameObject Fire_Mage_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On_Buy_Button(int Type)
    {
        Character_Type Char_Type = (Character_Type)Type;
        
        switch (Char_Type)
        {
            case Character_Type.Air:
                Instantiate(Air_Mage_Prefab, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case Character_Type.Fire:
                Instantiate(Fire_Mage_Prefab, new Vector3(0, 0, 0), Quaternion.identity);
                break;
        }
        
    }
}
