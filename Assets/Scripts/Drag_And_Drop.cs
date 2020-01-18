using UnityEngine;
using System.Collections;

public class Drag_And_Drop : MonoBehaviour
{
    private bool _mouse_state;
    private GameObject _target;
    public Vector3 screen_space;
    public Vector3 offset;
    private Vector3 original_pos;
    private bool has_original_pos = false;

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = !_mouse_state;

        if (Input.GetMouseButtonDown(0))// TODO && Game.Instance.state == Game.State.Buying)
        {

            RaycastHit hit_info;
            _target = Get_clicked_object(out hit_info);
            if (_target)
            {
                _mouse_state = true;
                screen_space = Camera.main.WorldToScreenPoint(_target.transform.position);
                offset = _target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_space.z));
                if (!has_original_pos)
                {
                    original_pos = _target.transform.position;
                    has_original_pos = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_target)
            {
                _mouse_state = false;
                Drop(_target);
               
            }
        }
        if (_mouse_state)
        {
            var offset_test = offset;
            offset_test.x *= 2;
            var cur_screen_space = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_space.z);
            var cur_position = Camera.main.ScreenToWorldPoint(cur_screen_space) + offset_test;
            cur_position.y = _target.transform.position.y;
            _target.transform.position = cur_position;
        }
    }

    public void Drop(GameObject target)
    {
        //check tile la plus proche
        GameObject closest_tile = Find_closest_tile(target.transform.position);
        Tile tuile = closest_tile.GetComponent<Tile>();
        if (target.GetComponent<Character>() != null)
        {
            Character character = target.GetComponent<Character>();
            Vector3 new_spot = (has_original_pos) ? original_pos : character.transform.position;
            has_original_pos = false;
            if (tuile.Is_available())
            {
                //quit old tile to new tile
                tuile.SetFoot(ref character);
                //new pos = milieux de la tile
                new_spot = closest_tile.transform.position + (new Vector3(0, 2, 0));
            }
            target.transform.position = new_spot;
        }
        else if (target.GetComponent<Tile>() != null)
        {
            Tile Tile_to_add = target.GetComponent<Tile>();
            if (tuile.is_replaceable)
            {
                Tile_to_add.transform.position = closest_tile.GetComponent<Transform>().position;
                Tile_to_add.is_placed = true;
                Destroy(closest_tile);
            }
            else
            {
                Tile_to_add.transform.position = (has_original_pos) ? original_pos : Tile_to_add.transform.position;
            }
        }
    }

    GameObject Get_clicked_object(out RaycastHit hit)
    {
        GameObject _target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            Tile tile = obj.GetComponentInParent<Tile>();
            if (obj.tag == "Mage" || (tile != null && tile.is_placed == false))
            {
                if (tile != null)
                {
                    _target = tile.gameObject;
                }else
                {
                    _target = obj;

                }
            }
        }

        return _target;
    }

    GameObject Find_closest_tile(Vector3 position)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Tile");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && !(curDistance < 0.1f))
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
