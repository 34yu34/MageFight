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

    public override string textify()
    {
        string text = "";
        text += ("Deep in The Maw is where particularly foolish Mages can meddle with the " +
            "forces of life and death. Tread lightly on the edge of the Abyss.\n\n");
        text += ("Amount of life stolen per attack increased by " + LIFESTEAL_BONUS*100 + "%");
        return text;
    }

    public override void remove_passive()
    {
        occupant.lifesteal.Remove_raw(LIFESTEAL_BONUS);
    }
}