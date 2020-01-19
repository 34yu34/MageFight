using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_Mage : Character
{
    public float att_speed_mult = 0.1f;
    private int number_buff = 0;
    Air_Mage()
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
        att_speed.Add_mult(0.1f);
        number_buff++;
    }

    public override void Reset_round()
    {
        base.Reset_round();
        for (int i = 0; i < number_buff; ++i)
        {
            att_speed.Remove_mult(0.1f);
        }
        number_buff = 0;

    }
}
