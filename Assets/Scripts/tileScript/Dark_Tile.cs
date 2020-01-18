using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark_Tile : Tile
{
    public float LIFESTEAL_BONUS = 0.15f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.lifesteal.Add_raw(LIFESTEAL_BONUS);
    }

    public override void remove_passive()
    {
        occupant.lifesteal.Remove_raw(LIFESTEAL_BONUS);
    }
}