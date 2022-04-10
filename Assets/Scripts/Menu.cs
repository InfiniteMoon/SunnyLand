using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : Viewbase
{

    public OptionPanel optionPanel;//选项界面
    public GameObject pauseMenu;
    public AudioMixer audioMixer;



    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void Optiongame()
    {
        //隐藏自己，显示选项

        //显示选项界面
        optionPanel.Show();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }


    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Mainvolume", value);
    }

    public void SetVolumeSound(float value)
    {
        audioMixer.SetFloat("Sound", value);
    }

    public void SetVolumeBgm( float value)
    {
        audioMixer.SetFloat("Bgm", value);
   
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
