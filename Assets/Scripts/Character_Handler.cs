﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Handler : MonoBehaviour
{
    public List<GameObject> mages;
    private static Character_Handler instance;

    public enum Chr_Mod_Stat
    {
        HEALTH, 
        MANA, 
        ATT_DAMAGE,
        ATT_SPEED,
        CRIT,
        CRIT_DMG,
        LIFESTEAL,
    }

    public static Character_Handler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Character_Handler>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Character give_random()
    {
        return Instantiate(mages[Random.Range(0, mages.Count)]).GetComponent<Character>();
    }

    public string get_stat_name(Chr_Mod_Stat stat)
    {
        if (stat == Chr_Mod_Stat.HEALTH)
        {
            return "Health";
        }
        if (stat == Chr_Mod_Stat.MANA)
        {
            return "Mana Cost";
        }
        if (stat == Chr_Mod_Stat.ATT_DAMAGE)
        {
            return "Attack Damage";
        }
        if (stat == Chr_Mod_Stat.ATT_SPEED)
        {
            return "Attack Speed";
        }
        if (stat == Chr_Mod_Stat.CRIT)
        {
            return "Critical Chances";
        }
        if (stat == Chr_Mod_Stat.CRIT_DMG)
        {
            return "Critical Damages";
        }
        if (stat == Chr_Mod_Stat.LIFESTEAL)
        {
            return "Lifesteal";
        }
        return "";
    }
}
