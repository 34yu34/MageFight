using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Damage_Popup_Controler : MonoBehaviour
{
    private GameObject Popup_Text_Parent;
    private GameObject canvas;

    private static Damage_Popup_Controler instance = null;

    public static Damage_Popup_Controler Instance {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Global").GetComponent<Damage_Popup_Controler>();
            }
            return instance;
        }
    }

    public void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!Popup_Text_Parent)
        {
            Popup_Text_Parent = Resources.Load("PopupTextParent", typeof(GameObject)) as GameObject;
        }
    }

    public void CreateFloatingText(string text, Transform location)
    {
        GameObject instance = Instantiate(Popup_Text_Parent);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos + (new Vector2(0,50));
        instance.GetComponentInChildren<Text>().text = text;
        Destroy(instance, 0.55f);
    }
}