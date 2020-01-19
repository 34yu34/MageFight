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

    public override string textify()
    {
        string text = "";
        text += ("Grueling heat, inhospitable conditions and sheer danger are the conditions " +
            "needed for the true power of a Mage's spells to shine.\n\n");
        text += ("Attack damage increased by " + ATT_BONUS * 100 + "%");
        return text;
    }

    public override void remove_passive()
    {
        occupant.att_damage.Remove_mult(ATT_BONUS); 
    }
}
