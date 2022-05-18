using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFire : MonoBehaviour
{
    //private Vector3 startPoint;
    private float SleepCoolDown;
    public float initSleepTime;

    // Start is called before the first frame update
    void Start()
    {
        //startPoint = transform.position;
        InitTime();
 
    }

    // Update is called once per frame
    void Update()
    {
        SleepCoolDown -= Time.deltaTime;
        //ReviveCoolDown -= Time.deltaTime;
        if(SleepCoolDown <= 0)
        {
            Sleep();
            InitTime();
        }
    }
    /*
    private void Revive()
    {
        transform.position = startPoint;
        gameObject.SetActive(true);
    }*/

    private void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void InitTime()
    {
        SleepCoolDown = initSleepTime - Random.Range(0, 10);
    }
}
