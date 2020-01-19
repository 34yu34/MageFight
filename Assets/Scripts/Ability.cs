using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Character target;
    public Character sender;
    public const float animation_time = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void set_target(Character target_in, float damage_in, Character sender_in)
    {

    }
}
