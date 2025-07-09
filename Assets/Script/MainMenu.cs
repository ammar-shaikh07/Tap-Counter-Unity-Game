using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Mainmenu;
    public GameObject Settings;
    public GameObject BtnSettings;
    public GameObject Cradits;
    public GameObject HowToPlay;
    public AudioSource GameAudio;
    public AudioClip clip;
    public Animator settingsAnimater;
    public GameObject FadePenal;

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();

        Mainmenu.SetActive(true);
        Settings.SetActive(false);
        Cradits.SetActive(false);
        HowToPlay.SetActive(false);
        Debug.Log("Start called");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnSettingsClicked()
    {
        Mainmenu.SetActive(false);
        Settings.SetActive(true);
        BtnSettings.SetActive(false);
        // PlayButtonSound();
        settingsAnimater.SetTrigger("slide-in");
        Debug.Log("BtnSettingsClicked");
    }

    public void BtnHelpClicked()
    {
        Mainmenu.SetActive(false);
        HowToPlay?.SetActive(true);
        // PlayButtonSound();
        Debug.Log("BtnHelpClicked");
    }

    public void BtnCraditsClicked()
    {
        Mainmenu.SetActive(false);
        Cradits.SetActive(true);
        // PlayButtonSound();
        Debug.Log("BtnCraditsClicked");
    }

    public void BtnBackClicked()
    {
        Mainmenu.SetActive(true);
        settingsAnimater.SetTrigger("slide-out");

        //Invoke("DesableSettingspenal", 1);
        StartCoroutine(DesableSettingspenal());
        Cradits.SetActive(false);
        HowToPlay.SetActive(false);
        //PlayButtonSound();
        Debug.Log("BtnBackClicked");
    }

    public IEnumerator DesableSettingspenal()
    {
        yield return new WaitForSeconds(1);
        Settings.SetActive(false);
        BtnSettings.SetActive(true);
    }

    public void BtnPlayClicked()
    {
        FadeOut();
        StartCoroutine(LoadLevelWithDelay());
       
    }

    public IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
    }


    public void PlayButtonSound()
    {
        // GameAudio.PlayOneShot(clip);
    }
    public void stateMusic()
    {
        GameAudio.Play();
        Debug.Log("Trying to a text sound");
    }

    void FadeIn()
    {
        FadePenal.GetComponent<Animator>().SetTrigger("Fade-in");
    }

    void FadeOut()
    {
        FadePenal.GetComponent<Animator>().SetTrigger("Fade-out");
    }
}