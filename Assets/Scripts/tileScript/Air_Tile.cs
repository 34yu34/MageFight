using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_Tile : Tile
{
    public float ATT_SPD_BONUS = 0.30f;
    // Start is called before the first frame update
    public override void add_passive(ref Character occupant)
    {
        occupant.att_speed.Add_mult(ATT_SPD_BONUS);
    }

    public override string textify()
    {
        string text = "";
        text += ("Mages who subject themselves to the blistering winds of the North find " +
            "themselves casting with speed never before seen!\n\n");
        text += ("Attack speed increased by " + ATT_SPD_BONUS*100 + "%");
        return text;
    }

    public override void remove_passive()
    {
        occupant.att_speed.Remove_mult(ATT_SPD_BONUS);
    }
}