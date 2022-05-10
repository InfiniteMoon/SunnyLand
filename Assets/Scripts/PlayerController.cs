using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool tapJump;
    private bool isOnGround;
    private bool isCrouch;
    private bool isHurt = false;
    private bool isUnderGround;
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFalling;
    public bool isDashing;

    [Header("CD��UI���")]
    public Image cdImage;
    [Header("Dash����")]
    public float dashTime;//dashʱ��
    private float dashTimeLeft;//���ʣ��ʱ��
    private float lastDash = -10f;//��һ�γ��ʱ���
    public float dashCoolDown;
    public float dashSpeed;


    private float playerGravity;
    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public float horizintalmove;
    public float facedirection;
    public float movey;
    public float climbSpeed;
    public LayerMask ground;
    public LayerMask ladder;
    public static int cherry = 0;
    public static int gem = 0;
    public static int candy = 0;
    public int jumpcount;
    public BoxCollider2D crouchcoll;
    public Text cherryNumber;
    public Text gemNumber;
    public Text candyNumber;
    //public AudioSource jumpAudio;
    //public AudioSource hurtAudio;
   // public AudioSource collectionAudio;
    public Transform cellingCheck;
    public BoxCollider2D myfeet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        crouchcoll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        myfeet = GetComponent<BoxCollider2D>();
        playerGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizintalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");
        tapJump = Input.GetButtonDown("Jump");
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouch = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                //����ִ��dash
                ReadyToDash();
            }
        }


        cdImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;


        isOnGround = coll.IsTouchingLayers(ground);
        isUnderGround = Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground);
        Jump();
        isLadder = coll.IsTouchingLayers(ladder);
        cherryNumber.text = cherry.ToString();
        gemNumber.text = gem.ToString();
        candyNumber.text = candy.ToString();
        Climb();
        CheckAirStatus();
        CheckLadder();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        Dash();
        if(isDashing)
        {
            return;
        }
    }

    void Movement()
    {
        //ƽ��
        if(horizintalmove != 0)
        {
            rb.velocity = new Vector2(horizintalmove * speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        Crouch();
    }

    void Jump()
    {
        if (isOnGround)
        {
            jumpcount = 2;
        }
        //��Ծ
        if (tapJump && isOnGround && crouchcoll.enabled==true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //jumpAudio.Play();
           // SoundManager.instance.JumpAudio();
            anim.SetBool("jumping", true);
            jumpcount -= 1;
            tapJump = false;
        }
        if(tapJump && jumpcount > 1  && !isOnGround && crouchcoll.enabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //jumpAudio.Play();
            //SoundManager.instance.JumpAudio();
            anim.SetBool("jumping", true);
            jumpcount -= 1;
            tapJump = false;
        }
    }
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (isOnGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
            anim.SetBool("hurt", false);
            anim.SetFloat("running", 0.00f);
            anim.SetBool("crouching", false);
        }
        if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetBool("falling", false);
            anim.SetFloat("running", 0.00f);
            anim.SetBool("crouching", false);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
            }
        }
    }
    //��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ռ���Ʒ
        if (collision.tag == "CollectionCherry")
        {
            //collectionAudio.Play();
            SoundManager.instance.CollectionAudio();
            collision.GetComponent<Animator>().Play("IsGot");
        }
        if (collision.tag == "CollectionGem")
        {
            //collectionAudio.Play();
            SoundManager.instance.CollectionAudio();
            collision.GetComponent<Animator>().Play("IsGot");
        }
        if (collision.tag == "CollectionCandy")
        {
            //collectionAudio.Play();
            SoundManager.instance.CollectionAudio();
            collision.GetComponent<Animator>().Play("IsGot");
        }
        //�ش�Ч������
        if (collision.tag == "Spikes")
        {
            SoundManager.instance.HurtAudio();
            isHurt = true;

        }
        //��������
        if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            SoundManager.instance.FallAudio();
            Invoke("restart", 2f);
        }
    }

    //�������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                if ( transform.position.y > collision.transform.position.y //�ж�player�Ƿ���enemy֮��
                    && Mathf.Abs(transform.position.x - collision.transform.position.x) < 1)//�ж�player�Ƿ�Ӳ������
                {
                    enemy.jumpOn();
                    rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                    anim.SetBool("jumping", true);
                    tapJump = false;
                }
                else if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    //hurtAudio.Play();
                    SoundManager.instance.HurtAudio();
                    rb.velocity = new Vector2(-4, rb.velocity.y);
                    isHurt = true;
                }
                else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    //hurtAudio.Play();
                    SoundManager.instance.HurtAudio();
                    rb.velocity = new Vector2(4, rb.velocity.y);
                    isHurt = true;
                }
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                //hurtAudio.Play();
                SoundManager.instance.HurtAudio();
                rb.velocity = new Vector2(-2, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                //hurtAudio.Play();
                SoundManager.instance.HurtAudio();
                rb.velocity = new Vector2(2, rb.velocity.y);
                isHurt = true;
            }
        }
    }
    void Crouch()
    {
        //PlayerCrouching\
        
        if (isCrouch)
        {
            rb.velocity = new Vector2(horizintalmove * speed, rb.velocity.y);
            anim.SetBool("crouching", true);
            crouchcoll.enabled = false;
        }
        if (!isUnderGround && !isCrouch)//����bug��
        {
            anim.SetBool("crouching", false);
            crouchcoll.enabled = true;
        }
        if (isUnderGround && !isCrouch)//����bug��
        {
            anim.SetBool("crouching", true);
            crouchcoll.enabled = false;
        }
    }
    //����Ƿ���������
    void CheckLadder()
    {
        isLadder = myfeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    //��������
    void Climb()
    {
        if(isLadder)
        {
            float movey = Input.GetAxis("Vertical");
            if(movey > 0.5f||movey < -0.5f||isFalling)
            {
                anim.SetBool("ClimbIdle", false);
                anim.SetBool("Climbing", true);
                rb.gravityScale = 0.0f;
                rb.velocity = new Vector2(rb.velocity.x, movey * climbSpeed);
            }
            else
            {
                anim.SetBool("ClimbIdle", true);
                if(isJumping)
                {
                    anim.SetBool("Climbing", false);
                    anim.SetBool("ClimbIdle", false);
                }
                else 
                {
                    anim.SetBool("Climbing", false);
                    rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            anim.SetBool("Climbing", false);
            anim.SetBool("ClimbIdle", false);
            rb.gravityScale = playerGravity;
        }
    }

    void CheckAirStatus()
    {
        isJumping = anim.GetBool("Jump");
        isFalling = anim.GetBool("Fall");
        isClimbing = anim.GetBool("Climbing");
    }
    void restart()
    {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void ReadyToDash()
    {
        isDashing = true;

        dashTimeLeft = dashTime;

        lastDash = Time.time;

        cdImage.fillAmount = 1;
    }
    void Dash()
    {
        if(isDashing)
        {
            if(dashTimeLeft >0.001)
            {
                if(rb.velocity.y >0 && !isOnGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizintalmove, jumpforce);
                }
                rb.velocity = new Vector2(dashSpeed * horizintalmove, rb.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.GetFormPool();
            }
            if (dashTimeLeft <= 0.001)
            {
                isDashing = false;
                if (!isOnGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizintalmove, jumpforce);
                }
            }
        }
    }


    public void cherryCount()
    {
        cherry+=1;
    }

    public void gemCount()
    {
        gem+=1;
    }

    public void candyCount()
    {
        candy += 1;
    }
}
