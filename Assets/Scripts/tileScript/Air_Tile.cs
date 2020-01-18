using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_Tile : Tile
{
    public float ATT_SPD_BONUS = 0.30f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.att_speed.Add_mult(ATT_SPD_BONUS);
    }

    public override void remove_passive()
    {
        occupant.att_speed.Remove_mult(ATT_SPD_BONUS);
    }
}