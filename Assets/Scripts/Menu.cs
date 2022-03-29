using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
  public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void setVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}
