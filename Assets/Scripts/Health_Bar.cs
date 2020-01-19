using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar : MonoBehaviour
{
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float denominator = GetComponentInParent<Character>().health.basic;
        denominator = (denominator == 0) ? 1 : denominator;
        localScale.y = 2 * GetComponentInParent<Character>().health.curr / denominator;
        transform.localScale = localScale;
    }
}
