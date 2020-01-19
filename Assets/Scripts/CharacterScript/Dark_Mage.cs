using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark_Mage : Character
{
    public float damage_mult = 2.0f;
    private Stat damage;
    public Dark_Mage()
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
        target.Take_damage(att_damage.curr * damage_mult);
        heal(att_damage.curr * damage_mult);
    }
}
