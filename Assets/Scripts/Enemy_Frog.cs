using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private float leftx, rightx;
    //private Animator anim;
    private Collider2D coll;
    public float speed, jumpforce;
    public LayerMask ground;
    private bool faceleft = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switchAnim();
    }

    void movement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("jumping", true);
            if (faceleft)
            {
                if (transform.position.x < leftx)
                {
                    rb.velocity = new Vector2(speed, jumpforce);
                    transform.localScale = new Vector2(-1, 1);
                    faceleft = false;
                }
                else
                {
                    rb.velocity = new Vector2(-speed, jumpforce);
                }
            }
            else
            {
                if (transform.position.x > rightx)
                {
                    rb.velocity = new Vector2(-speed, jumpforce);
                    transform.localScale = new Vector2(1, 1);
                    faceleft = true;
                }
                else
                {
                    rb.velocity = new Vector2(speed, jumpforce);
                }
            }
        }
    }
    void switchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }
}
