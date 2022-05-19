using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartgame : MonoBehaviour
{
    // Start is called before the first frame update
    public void restart()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Myquit()
    {
        Application.Quit();
    }
}
