using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool is_on_board;

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
        
    }
}
