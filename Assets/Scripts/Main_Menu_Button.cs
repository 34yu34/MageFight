using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_Button : MonoBehaviour
{

    public Button play, quit;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(Play);
        quit.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene("GameBoard");
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
