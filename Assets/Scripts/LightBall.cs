using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    public GameObject Boss;
    public Collider2D collider2d;
    private Vector3 dir;//��ʼ�ķ���
    public float Speed;//�ٶ�
    public float LifeTime;//����ʱ��

    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        dir = transform.localScale;
    }

    void Update()
    {
        Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void Move()//����
    {
        if (Boss.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Boss.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//��ײ������
    {
    if (collision.CompareTag("Ground"))//�������汬ը
        {
            collider2d.enabled = false;
            Destroy();
        }
    }
}
