using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Tile : Tile
{
    public float CRIT_BONUS = 0.15f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.crit_chance.Add_raw(CRIT_BONUS);
    }

    public override void remove_passive()
    {
        occupant.crit_chance.Remove_raw(CRIT_BONUS);
    }
}
