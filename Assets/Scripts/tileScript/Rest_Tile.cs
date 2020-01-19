using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest_Tile : Tile
{
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.energy_remover.Add_mult(-2.0f);
    }

    public override string textify()
    {
        string text = "";
        text += ("Welcome to the Ethereal Plane, most well loved resting place " +
            "of all of the practitioners of the Arcane Arts.\n\n");
        text += ("Only tiles that allow Stamina regeneration.");
        return text;
    }

    public override void remove_passive()
    {
        occupant.energy_remover.Remove_mult(-2.0f);
    }
}
