using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Mage: Character
{
    public float Damage_debuff_mult = 0.25f;
    private List<Character> debuff_chars = new List<Character>();

    public Earth_Mage()
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
        target.att_damage.Add_mult(-Damage_debuff_mult);
        debuff_chars.Add(target);
    }

    public override void Reset_round()
    {
        foreach(Character chr in debuff_chars)
        {
            chr.att_damage.Remove_mult(-Damage_debuff_mult);
        }
        debuff_chars = new List<Character>();
        base.Reset_round();
    }
}

