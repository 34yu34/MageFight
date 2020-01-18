using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Tile : Tile
{
    public float ATT_BONUS = 0.20f; 
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.att_damage.Add_mult(ATT_BONUS);
    }

    public override void remove_passive()
    {
        occupant.att_damage.Remove_mult(ATT_BONUS); 
    }
}
