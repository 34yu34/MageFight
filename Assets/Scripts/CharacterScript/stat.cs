﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float basic;
    public float curr;
    private float actual;
    private List<float> _bonus_mult;
    private List<float> _bonus_raw;
    [System.NonSerialized]
    public bool no_neg;

    public Stat(float basic_val = 0.0f)
    {
        set_val(basic_val);
    }

    public void set_val(float val)
    {
        basic = val;
        curr = val;
        actual = val;
        _bonus_mult = new List<float>();
        _bonus_raw = new List<float>();
    }

    public Stat Add_mult(float val)
    {
        _bonus_mult.Add(val);
        return calc_actual();
    }

    public Stat Add_raw(float val)
    {
        _bonus_raw.Add(val);
        return calc_actual();
    }

    public void Remove_mult(float val)
    {
        _bonus_mult.Remove(val);
        calc_actual();
    }

    public void Remove_raw(float val)
    {
        _bonus_raw.Remove(val);
        calc_actual();
    }

    private Stat calc_actual()
    {
        float current_over_actual = actual == 0 ? 1 : curr / actual;

        float mult = 1; // mult by 1 stays the same
        for (int i = 0; i < _bonus_mult.Count; ++i)
        {
            mult += _bonus_mult[i];
        }

        // raw bonus
        float sum = 0;
        for (int i = 0; i < _bonus_raw.Count; ++i)
        {
            sum += _bonus_raw[i];
        }

        float new_actual = mult * basic + sum;
        if (no_neg)
        {
            new_actual = new_actual > 0.1f * basic ? new_actual : 0.1f * basic; 
        }
        actual = new_actual;
        curr = current_over_actual * actual;
        return this;
    }

    public void reset_curr()
    {
        calc_actual();
    }

    public float Actual()
    {
        return actual;
    }

    public float bonus()
    {
        return actual - basic;
    }
}
