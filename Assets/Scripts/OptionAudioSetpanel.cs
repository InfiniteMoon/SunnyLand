using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionAudioSetpanel : Viewbase
{
    public Slider slider_music;//背景音乐
    public Slider slider_sound;//音效

    public void OnMusicValueChange(float f)
    {   //对音量大小进行保存
        PlayerPrefs.SetFloat(Const.MusicVolume, f);
        //修改音量大小
    }

    public void OnSoundValueChange(float f)
    {   //对音量大小进行保存
        PlayerPrefs.SetFloat(Const.SoundVolume, f);
        //修改音量大小

    }

    public override void Show()
    {
        base.Show();
    }
}
