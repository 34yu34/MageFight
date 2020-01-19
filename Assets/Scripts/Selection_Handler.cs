using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Handler : MonoBehaviour
{
    [SerializeField]
    private Character char_selection;
    [SerializeField]
    private Tile tile_selection;
    private static Selection_Handler instance;

    public static Selection_Handler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Selection_Handler>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit_info;
            Get_clicked_object(out hit_info);
            Debug.Log(hit_info);

        }
    }

    public void set_selection(GameObject obj)
    {
        if (obj.GetComponent<Character>() != null)
        {
            char_selection = obj.GetComponent<Character>();
            tile_selection = null;
        }
        else if (obj.GetComponentInParent<Tile>() != null)
        {
            tile_selection = obj.GetComponentInParent<Tile>();
            char_selection = null;
        }
        else
        {
            char_selection = null;
            tile_selection = null;
        }
    }

    public string get_selection_text()
    {
        if (char_selection != null)
        {
            return char_selection.textify();
        }
        else if (tile_selection != null)
        {
            return tile_selection.textify();
        }
        return null;
    }

    public GameObject get_selection()
    {
        if(char_selection != null)
        {
            return char_selection.gameObject;
        }
        else if (tile_selection != null)
        {
            return tile_selection.gameObject;
        }

        return null;
    }

    public Sprite get_selection_sprite()
    {
        if (char_selection != null)
        {
            return char_selection.get_sprite();
        }
        else if (tile_selection != null)
        {
            return tile_selection.get_sprite();
        }
        return null;
    }

    void Get_clicked_object(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            set_selection(hit.collider.gameObject);
        }
    }
}
