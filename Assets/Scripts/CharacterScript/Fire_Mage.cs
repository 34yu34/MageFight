using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Mage : Character
{
    public float DAMAGE_MULT = 3.0f;
    public Fire_Mage()
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
        List<Character> ennemies = Game.Instance.Get_other_player(owner).characters;

        foreach(Character bob in ennemies)
        {
            if ((target.transform.position - bob.transform.position).sqrMagnitude < 81.0f)
            {
                bob.Take_damage(att_damage.curr * DAMAGE_MULT);
            }
        }
    }
}
