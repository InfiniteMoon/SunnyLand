using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionAudioSetpanel : Viewbase
{
    public Slider slider_music;//��������
    public Slider slider_sound;//��Ч

    public void OnMusicValueChange(float f)
    {   //��������С���б���
        PlayerPrefs.SetFloat(Const.MusicVolume, f);
        //�޸�������С
    }

    public void OnSoundValueChange(float f)
    {   //��������С���б���
        PlayerPrefs.SetFloat(Const.SoundVolume, f);
        //�޸�������С

    }

    public override void Show()
    {
        base.Show();
        slider_music.value = PlayerPrefs.GetFloat(Const.MusicVolume, 0);
        slider_sound.value = PlayerPrefs.GetFloat(Const.SoundVolume, 0);
    }
}
