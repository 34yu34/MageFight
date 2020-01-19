using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Bar : MonoBehaviour
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
        localScale.y = 2 * GetComponentInParent<Character>().energy.curr / GetComponentInParent<Character>().energy.basic;
        transform.localScale = localScale;
    }
}
