using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class workscore : MonoBehaviour
{
        [SerializeField] TMP_Text highscoreText;
    void Start(){
        if (!PlayerPrefs.HasKey("highScore"))
        {
          PlayerPrefs.SetInt("highScore", 0);
        }
        else
        {
            highscoreText.text="High Score: "+PlayerPrefs.GetInt("highScore");
        }
    } 
    
}
