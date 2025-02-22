using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenu : MonoBehaviour
{

   public void playgame()
    {
        SceneManager.LoadScene("main");

    }
    public void retry(){
        SceneManager.LoadScene("main");
    }
    public void mm(){
        SceneManager.LoadScene("mainmenu");
    }
    public void exit(){
        Application.Quit();
    }
}
