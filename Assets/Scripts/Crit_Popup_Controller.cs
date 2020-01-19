using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crit_Popup_Controller : MonoBehaviour
{
private GameObject Popup_Text_Parent;
    private GameObject canvas;

    private static Crit_Popup_Controller instance = null;

    public static Crit_Popup_Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Crit_Popup_Controller>();
            }
            return instance;
        }
    }

    public void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!Popup_Text_Parent)
        {
            Popup_Text_Parent = Resources.Load("PopupTextParent_Crit", typeof(GameObject)) as GameObject;
        }
    }

    public void CreateFloatingText(Transform location)
    {
        GameObject instance = Instantiate(Popup_Text_Parent);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos + (new Vector2(0, 50));
        Destroy(instance, 0.55f);
    }
}
