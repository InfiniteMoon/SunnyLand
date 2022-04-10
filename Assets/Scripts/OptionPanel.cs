using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : Viewbase
{
    public Menu menuPanel;//�˵�����
    public OptionAudioSetpanel optionAudioSetpanel;//ѡ��  ��������
    public OptionOperatorPanel optionOperatorPanel;//ѡ��  ����˵��
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
        //��ʾ��������
        optionAudioSetpanel.Show();
    }

    public void OnOperatorClick()
    {
        HideOrShowOptionPanel(false);
        //��ʾ����˵��
        optionOperatorPanel.Show();
    }

    public void OnBackClick()
    {
        if(optionAudioSetpanel.IsShow()||optionOperatorPanel.IsShow())
        {
            optionAudioSetpanel.Hide();
            optionOperatorPanel.Hide();
            //��ʾoption����
            HideOrShowOptionPanel(true);
        }
        else
        {
            //�����Լ�
            this.Hide();
            //��ʾmenu
            menuPanel.Show();
        }
       
    }



    //��ʾ�����ؽ���
    void HideOrShowOptionPanel(bool isShow)
    {

        btn_audio.SetActive(isShow);
        btn_operator.SetActive(isShow);
        messagepanel.SetActive(isShow);
    }



}