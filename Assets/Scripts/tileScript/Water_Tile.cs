using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Tile : Tile
{
    public float MANA_ON_ATTACK_MULT = 0.5f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.mana_on_attack.Add_mult(MANA_ON_ATTACK_MULT);
    }

    public override string textify()
    {
        string text = "";
        text += ("The monks once said that the flow of life could be felt in the flow of the river. " +
            "Mages who follow this proverb rarely find themselves lacking in Mana.\n\n");
        text += ("Mana received per attack increased by " + MANA_ON_ATTACK_MULT*100 + "%");
        return text;
    }

    public override void remove_passive()
    {
        occupant.mana_on_attack.Remove_mult(MANA_ON_ATTACK_MULT);
    }
}
