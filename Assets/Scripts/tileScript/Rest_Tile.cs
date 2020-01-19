using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest_Tile : Tile
{
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.energy_remover.Add_mult(-1.0f);
    }

    public override void remove_passive()
    {
        occupant.energy_remover.Remove_mult(-1.0f);
    }
}
