using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewbase : MonoBehaviour
{
    public virtual void Show()
    {
        transform.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        transform.gameObject.SetActive(false);
    }
    //�ж��Ƿ���ʾ
    public bool IsShow()
    {
        return gameObject.activeSelf;
    }
}
