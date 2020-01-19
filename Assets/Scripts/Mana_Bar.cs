using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Bar : MonoBehaviour
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
        localScale.y = 2 * GetComponentInParent<Character>().mana.curr / GetComponentInParent<Character>().mana.basic;
        transform.localScale = localScale;
    }
}
