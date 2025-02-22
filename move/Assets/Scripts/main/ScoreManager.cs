using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public float increaseInterval = 1f;

    private float timer = 0f;

    public GameObject damagepopup;
    [SerializeField]
    Transform player;
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField] TMP_Text highscoreText;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            score++;
            timer = 0f;
        }

        scoreText.text = score + "";
    }

    public void incrementScore(int value, Vector3 yposition)
    {
        score += value;
        
        var obj =Instantiate(damagepopup, yposition, Quaternion.identity);
        obj.GetComponent<TMP_Text>().text = value+"";
        Debug.Log("h");
    }
    public void setHighScore(){
        int highScore=PlayerPrefs.GetInt("highScore");
        if(score>highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            highscoreText.text="New High Score: "+score;
        }
        else{
            highscoreText.text="Score: "+score;
        }

    }
}
