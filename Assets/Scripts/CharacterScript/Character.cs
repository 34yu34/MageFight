using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public GameObject ability_anim;

    public Player owner;
    public Timer timer = null;
    public Character target = null;
    public Tile tile = null;
    public GameObject projectile;

    public Stat health;
    public Stat mana;
    public Stat mana_on_attack;
    public Stat energy;
    public Stat energy_remover;
    public Stat att_damage;
    public Stat att_speed;
    public Stat crit_chance;
    public Stat crit_damage_mult;
    public Stat lifesteal;
    public static int COST = 2;
    public bool has_upgraded;

    public float[] debuff;
    public float[] energy_treshold;

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = new Stat(health.basic);
        mana = new Stat(mana.basic);
        mana.curr = 0;
        mana_on_attack = new Stat(mana_on_attack.basic);
        energy = new Stat(energy.basic);
        energy_remover = new Stat(energy_remover.basic);
        att_damage = new Stat(att_damage.basic);
        att_damage.no_neg = true;
        att_speed = new Stat(att_speed.basic);
        att_speed.no_neg = true;
        crit_chance = new Stat(crit_chance.basic);
        crit_damage_mult = new Stat(crit_damage_mult.basic);
        lifesteal = new Stat(lifesteal.basic);
        debuff = new float[3] { -0.2f, -0.3f, -0.4f };
        energy_treshold = new float[3] { 0.5f, 0.35f, 0.2f };
        gameObject.tag = "Mage";
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[5].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[6].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[7].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[8].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[9].enabled = false;
    }

    public virtual void Reset_round()
    {
        health.curr = health.Actual();
        mana.curr = 0;
        Remove_energy();
    }

    private void Remove_energy()
    {
        energy.curr -= energy_remover.curr;
        target = null;
        if (energy.curr <= energy.basic * energy_treshold[2])
        {
            Apply_permanent_debuff(debuff[2]);
        }
        else if (energy.curr <= energy.basic * energy_treshold[1])
        {
            Apply_permanent_debuff(debuff[1]);
        }
        else if (energy.curr <= energy.basic * energy_treshold[0])
        {
            Apply_permanent_debuff(debuff[0]);
        }
        else if (energy.curr > energy.basic)
        {
            energy.curr = energy.basic;
        }
    }

    private void Apply_permanent_debuff(float debuff)
    {
        health.Add_mult(debuff);
        mana_on_attack.Add_mult(debuff);
        mana.Add_mult(-debuff);
        att_damage.Add_mult(debuff);
        att_speed.Add_mult(debuff);
        crit_chance.Add_mult(debuff);
        crit_damage_mult.Add_mult(debuff);
    }

    public void Take_damage(float dmg)
    {
        if (!Is_alive())
        {
            return;
        }

        Damage_Popup_Controler.Instance.Initialize();
        Damage_Popup_Controler.Instance.CreateFloatingText(Mathf.Floor(dmg).ToString(), transform);

        health.curr -= dmg;
        if (health.curr <= 0)
        {
            health.curr = 0;
            //play death sound
            GameObject sound_maker = GameObject.Find("sound_maker");
            if(sound_maker)
            {
                sound_maker.GetComponent<Sound>().Die();
            }

            gameObject.SetActive(false);
        }
    }

    public void heal(float life)
    {
        health.curr += life;
        health.curr = health.curr < health.Actual() ? health.curr : health.Actual();

        Heal_Popup_Controler.Instance.Initialize();
        Heal_Popup_Controler.Instance.CreateFloatingText(Mathf.Floor(life).ToString(), transform);
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
        bool has_crit = (Random.Range(0.0f, 1.0f) < crit_chance.curr);
        if (has_crit)
        {
            Crit_Popup_Controller.Instance.Initialize();
            Crit_Popup_Controller.Instance.CreateFloatingText(transform);
            
            //play critical sound
            GameObject sound_maker = GameObject.Find("sound_maker");
            if (sound_maker)
            {
                sound_maker.GetComponent<Sound>().Do_critical();
            }

        }
        return has_crit ? (int)((float)att_damage.curr * crit_damage_mult.curr) : att_damage.curr;
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
            Instantiate(projectile, GetComponent<Transform>().position, GetComponent<Transform>().rotation).GetComponent<Projectile>().set_target(target, Calculate_damage(), this);
            mana.curr += mana_on_attack.curr;
            GetComponent<AudioSource>().Play();
            if (mana.curr >= mana.Actual())
            {
                Launch_ability();
                mana.curr = mana.curr - mana.Actual() ;

            }

            Invoke("Launch_attack", (1.0f / att_speed.curr));
        }
        else
        {
            target = null;
        }
    }

    public virtual void Launch_ability()
    {
    }

    public void Find_target()
    {
        List<Character> ennemies = Game.Instance.Get_other_player(owner).characters;
        float distance = 3000000.0f;


        for (int i = 0; i < ennemies.Count; ++i)
        {
            float new_distance = (ennemies[i].GetComponent<Transform>().position - GetComponent<Transform>().position).sqrMagnitude;
            if (new_distance < distance && ennemies[i].Is_alive() && ennemies[i].Is_on_board())
            {
                target = ennemies[i];
                distance = new_distance;
            }
        }
    }

    public string textify()
    {
        string text = "";
        text += (string.Join(" ", (name.Split('(')[0]).Split('_')) + '\n');
        text += textify_line("Attack Damage", att_damage);
        text += textify_line("Attack Speed", att_speed);
        text += textify_line("Health", health);
        text += textify_line("Mana", mana);
        text += textify_line("Energy", energy);
        text += textify_line("Critical Chances", crit_chance);
        text += textify_line("Lifesteal", lifesteal);
        return text;

    }

    public Sprite get_sprite()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private string textify_line(string name, Stat stat)
    {
        return name + " : " + stat.basic + " (" + stat.bonus() + ")\n";
    }

    public virtual int price()
    {
        return COST;
    }

    public Stat get_stat(Character_Handler.Chr_Mod_Stat stat)
    {
        switch (stat)
        {
            case Character_Handler.Chr_Mod_Stat.HEALTH:
                return health;
            case Character_Handler.Chr_Mod_Stat.MANA:
                return mana;
            case Character_Handler.Chr_Mod_Stat.ATT_DAMAGE:
                return att_damage;
            case Character_Handler.Chr_Mod_Stat.ATT_SPEED:
                return att_speed;
            case Character_Handler.Chr_Mod_Stat.CRIT:
                return crit_chance;
            case Character_Handler.Chr_Mod_Stat.CRIT_DMG:
                return crit_damage_mult;
            case Character_Handler.Chr_Mod_Stat.LIFESTEAL:
                return lifesteal;
        }
        return null;
    }

    /*/////////////////////////FIRE/////////////////////////////*/
    public void receive_attack_fire()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[0].enabled = true;
        Invoke("attack_fire_frame2", (0.2f));
        Invoke("attack_fire_frame3", (0.4f));
        Invoke("attack_fire_frame2", (0.6f));
        Invoke("attack_fire_end", (0.8f));
    }

    public void attack_fire_frame2()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
    }

    public void attack_fire_frame3()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[0].enabled = true;
    }

    public void attack_fire_end()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
    }

    /*/////////////////////////DARK/////////////////////////////*/
    public void receive_attack_dark()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;
        Invoke("attack_dark_frame2", (0.2f));
        Invoke("attack_dark_frame3", (0.4f));
        Invoke("attack_dark_frame2", (0.6f));
        Invoke("attack_dark_end", (0.8f));
    }

    public void attack_dark_frame2()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[3].enabled = true;
    }

    public void attack_dark_frame3()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;
    }

    public void attack_dark_end()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
    }

    /*/////////////////////////WIND/////////////////////////////*/
    public void receive_attack_wind()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[4].enabled = true;
        Invoke("attack_wind_frame2", (0.2f));
        Invoke("attack_wind_frame3", (0.4f));
        Invoke("attack_wind_frame2", (0.6f));
        Invoke("attack_wind_end", (0.8f));
    }
    public void attack_wind_frame2()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[5].enabled = true;
    }

    public void attack_wind_frame3()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[5].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[4].enabled = true;
    }

    public void attack_wind_end()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[5].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
    }

    /*/////////////////////////WATER/////////////////////////////*/
    public void receive_attack_water()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[6].enabled = true;
        Invoke("attack_water_frame2", (0.2f));
        Invoke("attack_water_frame3", (0.4f));
        Invoke("attack_water_frame2", (0.6f));
        Invoke("attack_water_end", (0.8f));
    }

    public void attack_water_frame2()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[6].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[7].enabled = true;
    }

    public void attack_water_frame3()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[7].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[6].enabled = true;
    }

    public void attack_water_end()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[7].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[6].enabled = false;
    }

    /*/////////////////////////EARTH/////////////////////////////*/
    public void receive_attack_earth()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[8].enabled = true;
        Invoke("attack_earth_frame2", (0.2f));
        Invoke("attack_earth_frame3", (0.4f));
        Invoke("attack_earth_frame2", (0.6f));
        Invoke("attack_earth_end", (0.8f));
    }

    public void attack_earth_frame2()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[8].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[9].enabled = true;
    }

    public void attack_earth_frame3()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[9].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[8].enabled = true;
    }

    public void attack_earth_end()
    {
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[9].enabled = false;
        ability_anim.GetComponentsInChildren<SpriteRenderer>()[8].enabled = false;
    }
}
