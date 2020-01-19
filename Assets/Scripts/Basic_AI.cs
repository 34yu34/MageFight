using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_AI : MonoBehaviour
{

    public enum Strategy
    {
        Tile,
        Character
    }

    public List<Tile> own_tiles;
    public List<Character> playing_char;
    public Player player;
    public Tile tile_to_place = null;
    private Strategy strategy = Strategy.Character;
    private Game.State last_state = Game.State.Reset; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (last_state != Game.Instance.state)
        {
            switch (Game.Instance.state)
            {
                case Game.State.Buying:
                    Buy();
                    break;

                case Game.State.Terrain:
                    Move_Ground();
                    break;

                case Game.State.Planning:
                    Place_Character();
                    break;

                case Game.State.Fighting:
                    make_visible(true);
                    break;
                
                case Game.State.Reset:
                    make_visible(false);
                    break;
            }
            last_state = Game.Instance.state;
        }
    }

    public virtual void Buy()
    {
        playing_char = new List<Character>();
        if (player.characters.Count < 2)
        {
            buy_character();
            buy_character();
        }
        else if (strategy == Strategy.Character)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (buy_character())
                {
                    break;
                }
            }
        }
        else if (strategy == Strategy.Tile)
        {
            buy_tile();
        }
    }

    public virtual bool buy_character()
    {
        Character chr = Character_Handler.Instance.give_random();
        if (chr.price() <= player.gold)
        {
            player.gold -= chr.price();
            player.characters.Add(chr);
            reroll_strat();
            return true;
        }
        return false;
    }

    public virtual bool buy_tile()
    {
        Tile tile = Tiles_Handler.Instance.give_random();
        if (tile.price() <= player.gold)
        {
            player.gold -= tile.price();
            tile_to_place = tile;
            reroll_strat();
            return true;
        }
        return false;
    }

    public virtual void reroll_strat()
    {
       strategy = Random.Range(0, 3) > 2 ? Strategy.Tile : Strategy.Character;
    }

    public virtual void Move_Ground()
    {
        if (tile_to_place != null)
        {
            if (own_tiles.Count > 11)
            {
                place_tile(Random.Range(0, own_tiles.Count - 1));
            }
            else
            {
                for(int i = 0; i < own_tiles.Count; ++i)
                {
                    if (own_tiles[i].GetComponents<Normal_Tile>() != null)
                    {
                        place_tile(i);
                        break;
                    }
                }
            }
        }
    }

    public virtual void place_tile(int index)
    {
        tile_to_place.transform.position = own_tiles[index].transform.position;
        tile_to_place.transform.rotation = own_tiles[index].transform.rotation;
        tile_to_place.is_placed = true;
        Destroy(own_tiles[index]);
        own_tiles[index] = tile_to_place;
    }

    public virtual void Place_Character()
    {
        for (int i = 0; i < player.characters.Count; ++i)
        {
            Character pl = player.characters[i];
            if (pl.energy.curr < 600.0f)
            {
                for(int j = 0; j < own_tiles.Count; ++j)
                {
                    if (own_tiles[j].GetComponents<Normal_Tile>() == null)
                    {
                        Tile tile = own_tiles[j];
                        pl.transform.position = tile.transform.position + (new Vector3(0, 2, 0));
                        tile.Occupy(ref pl);
                        playing_char.Add(pl);
                    }
                }

                for (int j = 0; j < own_tiles.Count; ++j)
                {
                     Tile tile = own_tiles[j];
                     pl.transform.position = tile.transform.position + (new Vector3(0, 2, 0));
                     tile.Occupy(ref pl);
                     playing_char.Add(pl);
                }
            }
        }
    }
   
    public void make_visible(bool is_visible)
    {
        foreach(Character pl in playing_char)
        {
            pl.gameObject.SetActive(is_visible);
        }
    }
}
