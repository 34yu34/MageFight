using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class upgrade
{
    public Character_Handler.Chr_Mod_Stat stat;
    public float val;
    public bool is_raw;

    void upgrade_chr(Character chr)
    {
        if (is_raw)
        {
            chr.get_stat(stat).Add_raw(val);

        }
        else
        {
            chr.get_stat(stat).Add_mult(val);
        }
    }
}
