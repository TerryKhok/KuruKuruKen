using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //初期時間スケールを設�?
    private void Start()
    {
        Time.timeScale = 1f;
    }

    //==========Scene転移==========
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        FindObjectOfType<AudioManager>().Stop("BGM1");
        FindObjectOfType<AudioManager>().Play("BGM2");
        // SceneManager.LoadScene("WeaponSelectScene_Lightsaber");
        SceneManager.LoadScene("WeaponSelectScene");
    }

    public void Option()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        SceneManager.LoadScene("CreditsScene");
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        Application.Quit();
    }
    //==============================
}
