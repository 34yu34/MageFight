using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play_Again : MonoBehaviour
{
    public Button yes;
    public Button no;
    // Start is called before the first frame update
    void Start()
    {
        yes.GetComponent<Button>().onClick.AddListener(LoadMain);
        no.GetComponent<Button>().onClick.AddListener(Quit);
    }

    void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
