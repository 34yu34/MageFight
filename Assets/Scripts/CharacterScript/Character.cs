using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    static float mana_on_attack = 10.0f;

    public bool is_on_board;
    public int team;
    public Timer timer = null;
    Character target;

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
    void Start()
    {
    }

    void Reset_round()
    {
        health.curr = health.Actual();
        mana.curr = 0;
        energy.curr -= energy_remover.curr;
    }

    void Take_damage(float dmg)
    {
        health.curr -= dmg;
        health.curr = health.curr> 0 ? health.curr : 0;
    }

    bool Is_alive()
    {
        return (health.curr > 0);
    }

    float Calculate_damage()
    {
        return (Random.Range(0.0f, 1.0f) < crit_chance.curr) ? (int)((float)att_damage.curr * crit_damage_mult.curr) : att_damage.curr;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Game.Instance.state)
        {
            case Game.State.Fighting:
                Fighting();
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
        if (target != null && Is_alive())
        {
            target.Take_damage(Calculate_damage()); // TODO Projectil
            mana.curr += mana_on_attack;
            if (mana.curr >= mana.Actual())
            {
                Launch_ability();
                mana.curr = 0;
            }

            if (!target.Is_alive())
            {
                target = null;
            }
            Invoke("Launch_attack", 1000 / att_speed.curr);
        }
    }

    public abstract void Launch_ability();

    public void Find_target()
    {
        List<Character> ennemies = Game.Instance.Get_other_player(team).characters;
        float distance = (ennemies[0].GetComponent<Transform>().position - GetComponent<Transform>().position).sqrMagnitude;
        target = ennemies[0];

        for(int i = 1; i < ennemies.Count; ++i)
        {
            float new_distance = (ennemies[i].GetComponent<Transform>().position - GetComponent<Transform>().position).sqrMagnitude;
            if (new_distance < distance && ennemies[i].Is_alive())
            {
                target = ennemies[i];
                distance = new_distance;
            }
        }
    }
}
