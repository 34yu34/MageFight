using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Tile : Tile
{
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
    }

    public override string textify()
    {
        string text = "";
        text += ("It's grass! What were you expecting to find here?\n\n");
        text += ("Seriously, it doesn't do anything.");
        return text;
    }

    public override void remove_passive()
    {
    }
}
