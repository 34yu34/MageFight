using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class upgrade
{
    public Character_Handler.Chr_Mod_Stat stat;
    public float val;
    public bool is_raw;

    public int COST = 1;

    public void upgrade_chr(Character chr)
    {
        if (is_raw)
        {
            chr.get_stat(stat).basic += val;
        }
        else
        {
            chr.get_stat(stat).basic *= (1+val);
        }
        chr.get_stat(stat).reset_curr();
        chr.has_upgraded = true;
    }

    public string text()
    {
        if (is_raw)
        {
            return "Add " + val + " " +Character_Handler.Instance.get_stat_name(stat);
        }
        else
        {
            return "Add " + val * 100.0f + "% " + Character_Handler.Instance.get_stat_name(stat);  
        }
    }

    public int price()
    {
        return COST;
    }
}
