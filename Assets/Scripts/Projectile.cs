using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Character target;
    public Character sender;
    public float damage;
    public float time;
    public const float animation_time = 0.75f;
    bool has_att = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void set_target(Character target_in, float damage_in, Character sender_in)
    {
        target = target_in;
        time = animation_time;
        damage = damage_in;
        has_att = false;
        sender = sender_in;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.Is_alive())
        {
            if (time > 0)
            {
                Vector3 distance = (target.GetComponent<Transform>().position - GetComponent<Transform>().position);
                GetComponent<Transform>().Translate((distance * Time.deltaTime / time));
                time -= Time.deltaTime;
            }
            else
            {
                if (!has_att)
                {
                    target.Take_damage(damage);
                    if (sender.Is_alive() && sender.lifesteal.curr > 0)
                    {
                        sender.heal(damage * sender.lifesteal.curr);
                    }
                    Destroy(gameObject);
                    has_att = true;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
