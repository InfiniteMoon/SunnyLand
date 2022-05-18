using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    public GameObject Boss;
    public Collider2D collider2d;
    private Vector3 dir;//初始的方向
    public float Speed;//速度
    public float LifeTime;//存在时间

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
    public void Move()//飞行
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
    private void OnTriggerEnter2D(Collider2D collision)//碰撞触发器
    {
    if (collision.CompareTag("Ground"))//碰到地面爆炸
        {
            collider2d.enabled = false;
            Destroy();
        }
    }
}
