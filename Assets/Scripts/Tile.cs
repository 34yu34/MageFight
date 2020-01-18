using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool is_playable;
    public bool is_dropable;
    public Character occupant = null;

    public bool SetFoot(ref Character newOccupant)
    {
        if(Is_available())
        {
            occupant = newOccupant;
            //TODO: give ability to occupant
            return true;
        }
        return false;
    }

    public bool Is_available()
    {
        return (occupant == null);
    }

    public void Leave()
    {
        occupant = null;
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
