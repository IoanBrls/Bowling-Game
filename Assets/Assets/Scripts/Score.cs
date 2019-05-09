using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public ScoreKeeper _ScoreKeeper;
    public Text frame;
    public Text ball;
    public Text score;
    public Text down;
    public Text frame2;
    public Text ball2;
    public Text score2;
    public Text down2;
    public Text StrikeAlert;
    public Text SpareAlert;
    public Text CheaterAlert;
    public Text Winner;
    public GameObject InGameMenu;


    public Image Plr1Arrow;
    public Image Plr2Arrow;

    public Light spotlight;
    private Color color1 = Color.red;
    private Color color2 = Color.blue;
    private float duration = 3.0f;
    private float timeleft = 1.5f;

    private void Start()
    {
        Time.timeScale = 1;
        StrikeAlert.text = null;
        SpareAlert.text = null;
        Plr1Arrow.enabled = true;
        Plr2Arrow.enabled = false;
        InGameMenu.SetActive(false);
    }

    

    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        spotlight.color = Color.Lerp(color1, color2, t);

        frame.text = "FRAME: " + _ScoreKeeper._frame.ToString();
        ball.text = "BALL: " + _ScoreKeeper._frameBall.ToString();
        score.text = "SCORE: " + _ScoreKeeper._Score.ToString();
        down.text = "DOWN: " + _ScoreKeeper._Down.ToString();
        frame2.text = "FRAME: " + _ScoreKeeper._frame2.ToString();
        ball2.text = "BALL: " + _ScoreKeeper._frameBall2.ToString();
        score2.text = "SCORE: " + _ScoreKeeper._Score2.ToString();
        down2.text = "DOWN: " + _ScoreKeeper._Down2.ToString();

        if (_ScoreKeeper.strike)
        {
            StrikeAlert.text = "!!!!~~~STRIKE~~~!!!!";
        }
        else
        {
            StrikeAlert.text = null;
        }

        if (_ScoreKeeper.spare)
        {
            SpareAlert.text = "~~~~~~~SPARE~~~~~~~~";
        }
        else
        {
            SpareAlert.text = null;
        }

        if(_ScoreKeeper.cheat)
        {
            CheaterAlert.text = " YOU CHEATER!!";
        }
        else
        {
            CheaterAlert.text = null;
        }

        if(_ScoreKeeper.plr1)
        {
            Plr1Arrow.enabled = true;
            Plr2Arrow.enabled = false;
        }
        else if (_ScoreKeeper.plr2)
        {
            Plr1Arrow.enabled = false;
            Plr2Arrow.enabled = true;
        }
        
        if(_ScoreKeeper.winner != 0)
        {
            if (_ScoreKeeper.winner == 1)
            {
                Winner.text = "WINNER IS PLAYER 1!!!";
            }
            else if (_ScoreKeeper.winner == 2)
            {
                Winner.text = "WINNER IS PLAYER 2!!!";
            }
            else if (_ScoreKeeper.winner == 3)
            {
                Winner.text = "TIE";
            }
            timeleft -= Time.deltaTime;
            if(timeleft<0)
            {
                Cursor.visible = true;
                SceneManager.LoadScene(0);
            }
        }
       
        if(Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0;
            InGameMenu.SetActive(true);
            Cursor.visible = true;
        }
    }

    
}
