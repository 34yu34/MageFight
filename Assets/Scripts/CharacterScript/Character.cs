using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Character_Enum;


public abstract class Character : MonoBehaviour
{
    static float mana_on_attack = 10.0f;

    public int team;
    public Character_Type Type;
    public Timer timer = null;
    public Character target = null;
    public Tile tile = null;
    public GameObject projectile;

    public Stat health;
    public Stat mana;
    public Stat energy;
    public Stat energy_remover;
    public Stat att_damage;
    public Stat att_speed;
    public Stat crit_chance;
    public Stat crit_damage_mult;
    public Stat lifesteal;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = new Stat(health.basic);
        mana = new Stat(mana.basic);
        energy = new Stat(energy.basic);
        energy_remover = new Stat(energy_remover.basic);
        att_damage = new Stat(att_damage.basic);
        att_speed = new Stat(att_speed.basic);
        crit_chance = new Stat(crit_chance.basic);
        crit_damage_mult = new Stat(crit_damage_mult.basic);
        lifesteal = new Stat(lifesteal.basic);
    }

    public void Reset_round()
    {
        health.curr = health.Actual();
        mana.curr = 0;
        energy.curr -= energy_remover.curr;
    }

    public void Take_damage(float dmg)
    {
        health.curr -= dmg;
        health.curr = health.curr> 0 ? health.curr : 0;
    }

    public bool Is_alive()
    {
        return (health.curr > 0);
    }

    public bool Is_on_board()
    {
        return tile != null && tile.is_playable;
    }

    float Calculate_damage()
    {
        return (Random.Range(0.0f, 1.0f) < crit_chance.curr) ? (int)((float)att_damage.curr * crit_damage_mult.curr) : att_damage.curr;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (Game.Instance.state)
        {
            case Game.State.Fighting:
                if (Is_on_board() && Is_alive())
                {
                    Fighting();
                }
                break;
            case Game.State.Buying:
                
                break;
        }
    }

    public void Fighting()
    {
        if (target == null)
        {
            Find_target();
            Launch_attack();
        }
    }

    public void Launch_attack()
    {
        if (target != null && this.Is_alive() && target.Is_alive())
        {
            Instantiate(projectile, GetComponent<Transform>().position, GetComponent<Transform>().rotation).GetComponent<Projectile>().set_target(target, Calculate_damage());
            mana.curr += mana_on_attack;
            if (mana.curr >= mana.Actual())
            {
                Launch_ability();
                mana.curr = 0;
            }

            /*if (!target.Is_alive())
            {
                target = null;
            }*/
            Invoke("Launch_attack", (1.0f / att_speed.curr));
        }
        else
        {
            target = null;
        }
    }

    public abstract void Launch_ability();

    public void Find_target()
    {
        List<Character> ennemies = Game.Instance.Get_other_player(team).characters;
        float distance = 3000000.0f;


        for(int i = 0; i < ennemies.Count; ++i)
        {
            float new_distance = (ennemies[i].GetComponent<Transform>().position - GetComponent<Transform>().position).sqrMagnitude;
            if (new_distance < distance && ennemies[i].Is_alive() && ennemies[i].Is_on_board())
            {
                target = ennemies[i];
                distance = new_distance;
            }
        }
        if(target == null)
        {
            Game.Instance.state = Game.State.Buying;
            //Game.Instance.Reset_characters();
        }
    }
}
