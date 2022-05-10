using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ifish : Enemy
{
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public bool Faceleft = true;
    public float Speed, JumpForce;
    public float leftx, rightx;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        // Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();


        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }


    void Movement()
    {
        if (Faceleft)//面向左侧
        {

            rb.velocity = new Vector2(-Speed, JumpForce);
            if (transform.position.x < leftx)//超过左侧的点转向
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, JumpForce);
            if (transform.position.x > rightx)//超过右侧的点转向
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }
}
