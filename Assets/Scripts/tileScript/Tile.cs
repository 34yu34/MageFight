using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public bool is_playable;
    public bool is_dropable;
    public bool is_placed;
    public bool is_replaceable;
    public Character occupant = null;

    public bool SetFoot(ref Character new_occupant)
    {
        if(Is_available())
        {
            occupant = new_occupant;
            add_passive(ref new_occupant);
            if (occupant.tile != null)
            {
                occupant.tile.remove_passive();
                occupant.tile.occupant = null;
            }
            occupant.tile = this;
            return true;
        }
        return false;
    }

    public abstract void add_passive(ref Character occupant);
    public abstract void remove_passive();

    public bool Is_available()
    {
        return (occupant == null);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
