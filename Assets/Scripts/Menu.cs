using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : Viewbase
{

    public OptionPanel optionPanel;//ѡ�����
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public GameObject dashIcon;


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
        //�����Լ�����ʾѡ��

        //��ʾѡ�����
        optionPanel.Show();
    }

    public void PauseGame()
    {
        Debug.Log("pause");
        dashIcon.SetActive(false);
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
        
        dashIcon.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
