using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gamemanager : MonoBehaviour
{
    public int TapCount;
    public GamePlayUI gamePlayUI;
    public float DefaultTimerValue;
    public float Timer;
    public bool TimerHasEnded = false;
    int TargetCount;
    public bool HasWon;
    public static int HighScore;
    public float CountDownTimer;
    public bool CountDownTimerHasEnded;
    int LavelNum = 1;
    int BaseLevelMultiplier = 10;

    void Start()
    {
        CountDownTimer = 3;
        CountDownTimerHasEnded = false;

        LoadGame();
        Timer = DefaultTimerValue;

        
         LavelNum = PlayerPrefs.GetInt("LevelNum",1);
        

        TargetCount = GetTapTargetCount(LavelNum);

        Debug.Log("Target count is:"+ TargetCount);

        gamePlayUI.RefreshLevelUI();


    }

    void Update()
    {
        if (CountDownTimerHasEnded == false)
        {
            CountDownTimer -= Time.deltaTime;
            if (CountDownTimer <= 0)
            {
                CountDownTimerHasEnded = true;
                gamePlayUI.DisableCountDownTimer();
                Debug.Log("CountDownTimer: " + CountDownTimerHasEnded);
               
            }
        }

        if (CountDownTimerHasEnded && !TimerHasEnded)
        {
            Timer -= Time.deltaTime;

            if (Timer < 0)
                Timer = 0;

            gamePlayUI.UpdateTimerText(Timer);

            if (Timer <= 0)
            {
                TimerHasEnded = true;
                Timer = 0;

                if (TapCount > TargetCount)
                {
                    HasWon = true;
                }
                else
                {
                    HasWon = false;
                }

                
                if (TapCount > HighScore)
                {
                    HighScore = TapCount;
                    Debug.Log("New HighScore: " + HighScore);
                }

               
                gamePlayUI.EndGame();
                gamePlayUI.UpdateHighScoreText();


                SaveGame();
                Debug.Log("New HighScore: " + HighScore);

                Debug.Log("TimerHasEnded: HasWon = " + HasWon.ToString());
                return;
            }


            if (gamePlayUI.ispaused == false && Input.GetMouseButtonDown(0))
            {
                TapCount++;
                gamePlayUI.UpdateTapCountText();
                Debug.Log("Click detected: " + TapCount);
            }
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("HighScore",HighScore);
        Debug.Log("HighScore Saved");
    }

    public void LoadGame()
    {
       HighScore = PlayerPrefs.GetInt("HighScore");
    }

    int GetTapTargetCount(int _LevelNum)
    {
        int _temp = 0;
        _temp = BaseLevelMultiplier * _LevelNum;
        return _temp;
    }

    public void LevelIncrease()
    {
        LavelNum++;

        PlayerPrefs.SetInt("LevelNum", LavelNum);

        TargetCount = GetTapTargetCount(LavelNum);
        gamePlayUI.RefreshLevelUI();
    }

    public int GetLelNum()
    {
        return LavelNum;
    }

    public int GetTargetCount()
    {
        return TargetCount;
    }

}
