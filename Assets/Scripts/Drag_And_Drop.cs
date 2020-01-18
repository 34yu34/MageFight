using UnityEngine;
using System.Collections;

public class Drag_And_Drop : MonoBehaviour
{
    private bool _mouse_state;
    private GameObject _target;
    public Vector3 screen_space;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = !_mouse_state;

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit_info;
            _target = Get_clicked_object(out hit_info);
            if (_target)
            {
                _mouse_state = true;
                screen_space = Camera.main.WorldToScreenPoint(_target.transform.position);
                offset = _target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_space.z));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_target)
            {
                _mouse_state = false;

                //check tile la plus proche
                GameObject closest_tile = Find_closest_tile(_target.transform.position);
                //new pos = milieux de la tile
                _target.transform.position = closest_tile.transform.position + (new Vector3(0, 2, 0));
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

    GameObject Get_clicked_object(out RaycastHit hit)
    {
        GameObject _target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if(hit.collider.gameObject.tag == "Mage")
                _target = hit.collider.gameObject;
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
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
