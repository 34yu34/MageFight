using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Tile : Tile
{
    public float HEALTH_BONUS = 300.0f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.health.Add_raw(HEALTH_BONUS);
    }

    public override void remove_passive()
    {
        occupant.health.Remove_raw(HEALTH_BONUS);
    }
}