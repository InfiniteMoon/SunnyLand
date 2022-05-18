using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bat;
    public AudioSource vic;
    private AudioSource bgm;
    public GameObject BOSS;
    private bool need2Sw = true;
    // Start is called before the first frame update
    void Start()
    {
        bgm = bat;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!BOSS.activeSelf)
        {
            if (need2Sw)
            {
                bgm.Pause();
                need2Sw = false;
            }
            //bgm.Pause();
            bgm = vic;
            if (!bgm.isPlaying)
            {
                bgm.Play();
            }

        }
    }
}
