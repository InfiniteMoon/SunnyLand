using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSSController : MonoBehaviour
{
    public int HP;
    private int HPmax;
    public Slider healthBar;
    public GameObject win;
    public GameObject HPBar;
    public AudioSource hurtAudio;
    public AudioSource deathAudio;
    public AudioSource bellAudio;
    public GameObject Ghost1, Ghost2;
    private Vector3 G1Start, G2Start;
    public float ghostFireCoolDown;
    private float GFCooolDowntemp;
    public GameObject lightballs;
    public float lightballCoolDown;
    private float LBCooolDowntemp;
    protected Animator anim;
    public GameObject healthLight;
    private bool canHealth = true;//是否能复活
    private bool needToHealth = false;//是否需要复活
    //public float healthTime;//有bug，超过三秒无法复活！
    public GameObject BOSS;//BOSS本体，连带碰撞盒
    //BOSS有限状态机
    public enum BossState//将所有状态一一列出来
    {
        LightBall,//光球
        Ghostfire,//僚机
        Health,//治疗
        Idle,//待机
        Hurt,//受伤
        Death,//死亡
    }
    BossState state;
    // Start is called before the first frame update
    void Start()
    {
        HPmax = HP;
        state = BossState.Idle;//将Boss的初始状态设置为Idle
        anim = GetComponent<Animator>();
        GFCooolDowntemp = ghostFireCoolDown + Random.Range(0, 5);//僚机cd初始化
        G1Start = Ghost1.transform.position;//僚机位置初始化
        G2Start = Ghost2.transform.position;
        LBCooolDowntemp = lightballCoolDown;//光球cd初始化
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        healthBar.value = HP;
        Timer();//计时器
        //Death();
        if (GFCooolDowntemp <= 0)
        {
            state = BossState.Ghostfire;
        }
        else if (LBCooolDowntemp <= 0)
        {
            state = BossState.LightBall;
        }
        switch (state)
        {
            case BossState.LightBall:
                {
                    LightBallSkill();
                    break;
                }
            case BossState.Ghostfire:
                {
                    GhostFire();

                    break;
                }
            case BossState.Health://里面全是bug，别碰
                {
                    //print(healthTime);
                    //if (healthTime <= 0)
                    //{
                    //    AnimHealthToIdle();
                    //}
                    HP = HPmax;
                    break;
                }
            case BossState.Idle:
                {
                    CanHealth();
                    if (needToHealth)
                    {
                        Health();
                    }
                    //anim.SetBool("Health", false);
                    //healthLight.SetActive(false);
                    break;
                }
            case BossState.Hurt:
                {
                    LBCooolDowntemp -= Random.Range(0, 4);
                    GFCooolDowntemp -= Random.Range(8, 15);
                    break;
                }
            case BossState.Death:
                {

                    break;
                }
        }
    }

    void Timer()//总计时器
    {
        GFCooolDowntemp -= Time.deltaTime;
        LBCooolDowntemp -= Time.deltaTime;
        //if(state == BossState.Health)
        //{
        //    healthTime -= Time.deltaTime;
        //}
    }

    void GhostFire()
    {

        Ghost1.transform.position = G1Start;
        Ghost2.transform.position = G2Start;
        Ghost1.SetActive(true);
        Ghost2.SetActive(true);
        if(state == BossState.Health)
        {
            GFCooolDowntemp = ghostFireCoolDown - Random.Range(10, 15);//重置计时器（复活时间内加速
        }
        else
        {
            AnimToAttack();
            GFCooolDowntemp = ghostFireCoolDown + Random.Range(0, 15);//重置计时器
        }


    }
    void AnimToAttack()
    {
        bellAudio.Play();
        anim.SetBool("Attack", true);
    }

    void AnimToIdle()
    {
        Debug.Log("hi");
        anim.SetBool("Hurt", false);
        anim.SetBool("Attack", false);
        state = BossState.Idle;
    }

    void AnimToHurt()
    {
        anim.SetBool("Hurt", true);
    }

    void AnimToHealth()
    {
        anim.SetBool("Health", true);
        healthLight.SetActive(true);
    }

    void AnimHealthToIdle()
    {
        Debug.Log("hello");
        HP = HPmax;
        anim.SetBool("Health", false);
        healthLight.SetActive(false);
        state = BossState.Idle;
    }

    void CreateLightBall()
    {
        for (int i = -5; i < 10; i++)
        {
            GameObject lightball = Instantiate(lightballs, null);
            lightball.SetActive(true);
            Vector3 dir = Quaternion.Euler(0, i * 45, 0) * -transform.right;
            lightball.transform.position = transform.position + dir * 1.0f;
            lightball.transform.rotation = Quaternion.Euler(0, 0, i * 45);
            //Debug.Log("test");
        }
    }

    public void LightBallSkill()
    {
        if(state != BossState.Health)//复活时间内不发射
        {
            AnimToAttack();
            CreateLightBall();
            state = BossState.Idle;
            LBCooolDowntemp = lightballCoolDown + Random.Range(0, 5);//重置计时器
        }

    }

    public void Hurt()
    {
        if(state != BossState.Health)//复活时间内无敌
        {
            state = BossState.Hurt;
            hurtAudio.Play();
            HP--;
            AnimToHurt();
        }
    }

    public void Health()
    {
        canHealth = false;
        AnimToHealth();
        state = BossState.Health;
    }

    public void CanHealth()
    {
        if(HP == 0 && canHealth == true)
        {
            needToHealth = true;
        }
        else
        {
            needToHealth = false;
        }
        if (HP == 0 && canHealth == false)
        {
            Death();
        }
    }

    public void Death()
    {
        //if (HP == 0 && canHealth == false)
        //{
        deathAudio.Play();
        state = BossState.Death;
        anim.SetTrigger("Death");
        //}
    }

    public void DestroyBOSS()
    {
        HPBar.SetActive(false);
        win.SetActive(true);
        BOSS.SetActive(false);
    }
}
