using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Mage : Character
{
    public static float heal_multiplier = 5.0f;

    public Water_Mage()
    {
    }
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void Launch_ability()
    {
        Character lowest = owner.characters[0]; 
        foreach (Character chr in owner.characters)
        {
            if (chr.Is_alive() && chr.health.curr < lowest.health.curr)
            {
                lowest = chr;
            }
        }
        lowest.heal(att_damage.curr * heal_multiplier);

    }
}
