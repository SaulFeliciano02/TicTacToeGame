using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitgame()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }

    public void multiplayer()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void singleplayer()
    {
        SceneManager.LoadScene("SingleMatchScene");
    }
}
