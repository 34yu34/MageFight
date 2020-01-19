using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buy_Menu : MonoBehaviour
{
    public const int NUMBER_BUTTON = 3;

    public List<GameObject> mages;
    public List<Button> buyers;

    public List<GameObject> terrains;
    public List<Button> terrains_buyers;
    public Vector3 terrain_spawn_position = new Vector3(30, 5, 17);

    public List<Button> upgrade_buyers;
    Character last_selection = null;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        round_reset();
    }

    public void round_reset()
    {

        foreach (Button b in buyers)
        {
            b.onClick.RemoveAllListeners();
            int index = Random.Range(0, mages.Count);
            b.enabled = true;
            b.GetComponentInChildren<Text>().text = string.Join(" ", mages[index].name.Split('_'));
            b.GetComponentsInChildren<Image>()[1].sprite = mages[index].GetComponentInChildren<SpriteRenderer>().sprite;
            b.GetComponentsInChildren<Text>()[1].text = (mages[index].GetComponent<Character>().price()).ToString();
            b.onClick.AddListener(delegate { On_Buy_Button(index, b); });
            if(Game.Instance.player1.characters.Count == 10)
            {
                b.enabled = false;
            }
        }
        List<int> indexes = new List<int>();
        foreach (Button b in terrains_buyers)
        {
            b.onClick.RemoveAllListeners();
            int index = Random.Range(0, terrains.Count);
            b.enabled = true;
            while (indexes.Contains(index) && indexes.Count < terrains.Count)
            {
                index = Random.Range(0, terrains.Count);
            }
            indexes.Add(index);
            b.GetComponentInChildren<Text>().text = terrains[index].name;
            b.GetComponentsInChildren<Image>()[1].sprite = terrains[index].GetComponent<Tile>().get_sprite();
            b.GetComponentsInChildren<Text>()[1].text = (terrains[index].GetComponent<Tile>().price()).ToString();
            b.onClick.AddListener(delegate { On_Buy_Terrain(index); });
        }

        foreach (Button b in upgrade_buyers)
        {
            b.onClick.RemoveAllListeners();
            b.enabled = true;

            int index = Random.Range(0, Upgrade_Handler.Instance.upgrades.Count-1);
            int index2 = index;
            while (index == index2)
            {
                index2 = Random.Range(0, Upgrade_Handler.Instance.upgrades.Count-1);
            }

            b.GetComponentInChildren<Text>().text = Upgrade_Handler.Instance.upgrades[index].text() + "\n" + Upgrade_Handler.Instance.upgrades[index2].text();
            b.GetComponentsInChildren<Text>()[1].text = (Upgrade_Handler.Instance.upgrades[index].price() + Upgrade_Handler.Instance.upgrades[index2].price()).ToString();
            b.onClick.AddListener(delegate { On_Buy_Upgrade(index, index2, b); });
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetComponent<CanvasGroup>().alpha = (Game.Instance.state == Game.State.Buying) ? 1 : 0;
        foreach (Button b in buyers)
        {
            if (!b.enabled)
            {
                b.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                b.GetComponent<Image>().color = new Color(125, 125, 125);
            }
        }
        foreach (Button b in terrains_buyers)
        {
            if (!b.enabled)
            {
                b.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                b.GetComponent<Image>().color = new Color(125, 125, 125);
            }
        }
        foreach (Button b in upgrade_buyers)
        {
            bool has_selection = Selection_Handler.Instance.get_selection() != null &&
                                 Selection_Handler.Instance.get_selection().GetComponent<Character>() != null &&
                                 !Selection_Handler.Instance.get_selection().GetComponent<Character>().has_upgraded;
            if (!has_selection)
            {
                b.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                b.GetComponent<Image>().color = new Color(125, 125, 125);
                last_selection = Selection_Handler.Instance.get_selection().GetComponent<Character>();
            }
        }
    }

    public void On_Buy_Button(int index, Button b)
    {
 
        GameObject[] GameObject_tiles = GameObject.FindGameObjectsWithTag("Tile");
        List<Tile> empty_tiles = new List<Tile>();
        foreach (GameObject GameObject_tile in GameObject_tiles)
        {
            Tile tile = GameObject_tile.GetComponent<Rest_Tile>();
            if (tile != null)
            {
                if (tile.Is_available() && !tile.is_playable)
                    empty_tiles.Add(GameObject_tile.GetComponent<Rest_Tile>());
            }
        }
        
        if (empty_tiles.Count > 0)
        {
            Vector3 spawn_position = empty_tiles[0].transform.position + (new Vector3(0,3,0));
            GameObject new_mage = null;
            new_mage = Instantiate(mages[index], spawn_position, Quaternion.identity);
            Character mage = new_mage.GetComponent<Character>();
            mage.Start();

            if (Game.Instance.player1.gold < mage.price())
            {
                Destroy(new_mage);
                return;
            }
            Game.Instance.player1.gold -= mage.price();

            mage.owner = Game.Instance.player1;
            Game.Instance.player1.characters.Add(mage);
            empty_tiles[0].Occupy(ref mage);
        }
        b.enabled = false;
    }

    public void On_Buy_Terrain(int index)
    {
        Game.Instance.player1.terrainNum++;
        GameObject new_terrain = Instantiate(terrains[index], terrain_spawn_position, Quaternion.identity);

        if (Game.Instance.player1.gold < new_terrain.GetComponent<Tile>().price())
        {
            Destroy(new_terrain);
            return;
        }
        Game.Instance.player1.gold -= new_terrain.GetComponent<Tile>().price();

        new_terrain.GetComponent<Tile>().is_placed = false;
        foreach (Button b in terrains_buyers)
        {
            b.enabled = false;
        }
    }

    public void On_Buy_Upgrade(int index, int index2, Button b)
    {
        Character bob = last_selection;
        Upgrade_Handler.Instance.upgrades[index].upgrade_chr(bob);
        Upgrade_Handler.Instance.upgrades[index2].upgrade_chr(bob);

        Game.Instance.player1.gold -= Upgrade_Handler.Instance.upgrades[index].price() + Upgrade_Handler.Instance.upgrades[index].price();
        b.onClick.RemoveAllListeners();
    }
}
