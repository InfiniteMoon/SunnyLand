using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerController>().candyCount();
        Destroy(gameObject);
    }

}
