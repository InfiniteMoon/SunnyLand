using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : Viewbase
{
    public Menu menuPanel;//菜单界面
    public OptionAudioSetpanel optionAudioSetpanel;//选项  音量设置
    public OptionOperatorPanel optionOperatorPanel;//选项  操作说明
    GameObject btn_audio;
    GameObject btn_operator;
    GameObject messagepanel;


    private void Start()
    {
        btn_audio= transform.Find("bg/btn_audio").gameObject;
        btn_operator=transform.Find("bg/btn_controller").gameObject;
        messagepanel = transform.Find("bg/MessagePanel").gameObject;
    }
    public void OnAudioClick()
    {
        HideOrShowOptionPanel(false);
        //显示音量设置
        optionAudioSetpanel.Show();
    }

    public void OnOperatorClick()
    {
        HideOrShowOptionPanel(false);
        //显示操作说明
        optionOperatorPanel.Show();
    }

    public void OnBackClick()
    {
        if(optionAudioSetpanel.IsShow()||optionOperatorPanel.IsShow())
        {
            optionAudioSetpanel.Hide();
            optionOperatorPanel.Hide();
            //显示option界面
            HideOrShowOptionPanel(true);
        }
        else
        {
            //隐藏自己
            this.Hide();
            //显示menu
            menuPanel.Show();
        }
       
    }



    //显示或隐藏界面
    void HideOrShowOptionPanel(bool isShow)
    {

        btn_audio.SetActive(isShow);
        btn_operator.SetActive(isShow);
        messagepanel.SetActive(isShow);
    }



}