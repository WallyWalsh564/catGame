using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinButton : MonoBehaviour
{

    public void OpenURL()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=zL19uMsnpSU");
    }

    public void quitGame()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
