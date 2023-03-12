using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float heart;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heart <= 0)
        {
            Destroy(gameObject);//Hủy quái
        }
    }
    
    public void TakeDame(float damage)
    {
        heart -= damage;
    }
}
