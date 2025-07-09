using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public Text TapCountText;
    public Gamemanager gameManager;
    public GameObject GameWin;
    public GameObject GameOver;
    public GameObject Pause;
    public Text TimerText;
    public Text HighScoreText;
    public bool ispaused;
    int CounterFontSize;
    public Text CountDownText;
    public Text TargetCountText;
    public Text LevelText;

    [Header("🔊 Audio")]
    public AudioSource GameAudio;
    public AudioClip ButtonClick;
    public AudioClip GamePlaySound;
    public AudioClip GameWinSound;
    public AudioClip GameOverSound;
    public AudioClip[] ButtonTapSound;

    void Start()
    {
        HighScoreText.text = "";
        CounterFontSize = TapCountText.GetComponent<Text>().fontSize;

        GameOver.SetActive(false);
        GameWin.SetActive(false);
        Pause.SetActive(false);

        
        if (GamePlaySound != null)
        {
            GameAudio.clip = GamePlaySound;
            GameAudio.loop = true;
            GameAudio.Play();
        }

        UpdateTargetText();
        UpdateLevelText();

        RefreshLevelUI();


    }

    void Update()
    {
        if (!gameManager.CountDownTimerHasEnded)
            CountDownText.text = Mathf.CeilToInt(gameManager.CountDownTimer).ToString();
    }

    public void DisableCountDownTimer()
    {
        CountDownText.gameObject.SetActive(false);
    }

    public void BtnBackClicked()
    {
        PlaySingleSound(ButtonClick);
        SceneManager.LoadScene(0);
    }

    public void UpdateTapCountText()
    {
        TapCountText.text = $" {gameManager.TapCount}";
        PlayRandomTapSound();
    }

    public void UpdateTimerText(float time)
    {
        TimerText.text = $"Time: {Mathf.CeilToInt(time)}";
    }

    public void UpdateHighScoreText()
    {
        HighScoreText.gameObject.SetActive(true);
        HighScoreText.text = $"HighScore: {Gamemanager.HighScore}";
    }

    public void EndGame()
    {
        UpdateHighScoreText();

        if (gameManager.HasWon)
        {
            GameWin.SetActive(true);
            PlaySingleSound(GameWinSound);
        }
        else
        {
            GameOver.SetActive(true);
            PlaySingleSound(GameOverSound);
        }
    }

    public void BtnReplayClicked()
    {
        PlaySingleSound(ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void BtnMainMenu()
    {
        PlaySingleSound(ButtonClick);
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
        Debug.Log("MainMenu Open");
    }

    public void pause()
    {
        PlaySingleSound(ButtonClick);
        Pause.SetActive(true);
        ispaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        PlaySingleSound(ButtonClick);
        Pause.SetActive(false);
        ispaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

   
    public void PlayRandomTapSound()
    {
        if (ButtonTapSound != null && ButtonTapSound.Length > 0)
        {
            int index = Random.Range(0, ButtonTapSound.Length);
            GameAudio.Stop(); 
            GameAudio.clip = ButtonTapSound[index];
            GameAudio.Play(); 
        }
    }

    
    private void PlaySingleSound(AudioClip clip)
    {
        if (clip == null || GameAudio == null) return;
        GameAudio.Stop();
        GameAudio.clip = clip;
        GameAudio.Play();
    }

    public void NextLevel()
    {
        gameManager.LevelIncrease();
        Debug.Log("Going To Next Level " + gameManager.GetLelNum());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateTargetText()
    {
        TargetCountText.text = "Target: " + gameManager.GetTargetCount();
    }

    public void UpdateLevelText()
    {
        LevelText.text = "Level: " + gameManager.GetLelNum();
    }

    public void RefreshLevelUI()
    {
        UpdateTargetText();
        UpdateLevelText();
    }


}
