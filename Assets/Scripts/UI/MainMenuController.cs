using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{


    private MainCamera mainCam;

    void Start()
    {

        mainCam = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        
    }

    public void playGame()
    {
        
        mainCam.gameStarted = true;

    }

    public void quitGame()
    {
        print("quit Game");
    }

}
