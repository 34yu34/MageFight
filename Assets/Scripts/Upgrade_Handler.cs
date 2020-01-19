using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Handler : MonoBehaviour
{
    private static Upgrade_Handler instance;

    public List<upgrade> upgrades;

    public static Upgrade_Handler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Upgrade_Handler>();
            }
            return instance;
        }
    }

    private void Start()
    {
        
    }
}
