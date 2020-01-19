using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Settings : MonoBehaviour
{
    public Button open;
    public Button cancel;
    public Text health;
    public Button inc_health;
    public Button dec_health;
    // Start is called before the first frame update
    void Start()
    {
        open.GetComponent<Button>().onClick.AddListener(OpenHealthMenu);
        cancel.GetComponent<Button>().onClick.AddListener(CloseHealthMenu);
        health.GetComponent<Text>().text = Global_Vars.health_setting.ToString();
        inc_health.GetComponent<Button>().onClick.AddListener(Inc_health);
        dec_health.GetComponent<Button>().onClick.AddListener(Dec_health);
    }

    void OpenHealthMenu()
    {
        open.GetComponentInParent<CanvasGroup>().alpha = 0;
        cancel.GetComponentInParent<CanvasGroup>().alpha = 1;
    }

    void CloseHealthMenu()
    {
        cancel.GetComponentInParent<CanvasGroup>().alpha = 0;
        open.GetComponentInParent<CanvasGroup>().alpha = 1;
    }

    void Inc_health()
    {
        health.GetComponent<Text>().text = (++Global_Vars.health_setting).ToString();
    }

    void Dec_health()
    {
        if (Global_Vars.health_setting > 1)
        {
            health.GetComponent<Text>().text = (--Global_Vars.health_setting).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
