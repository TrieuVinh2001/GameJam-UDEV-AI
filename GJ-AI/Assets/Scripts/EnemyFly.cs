﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    public float heart;

    private Rigidbody2D rb;
    private Animator anim;

    public float speed;

    private Transform player;

    public float shootingRange;
    public GameObject bulletParent;
    public GameObject bullet;
    public float fireRate = 1f;
    private float nextFireTime=0f;
    private BoxCollider2D box;

    void Start()
    {
        if (transform.position.x < 0)
        {
            speed = -speed;
            gameObject.transform.localScale = new Vector3(-2f, 2f, 1f);
        }
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (heart <= 0)
        {
            box.enabled = false;
            speed = 0;
            anim.SetBool("Die", true);
            Destroy(gameObject, 0.5f);//Hủy quái
        }
        if (player != null)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer <= shootingRange)
            {
                speed = 0;
                if (nextFireTime < Time.time)//Time.time là thời gian hiện tại
                {
                    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                    nextFireTime = Time.time + fireRate;//cộng thêm fireRate nghĩa là đợi thêm 1 lúc nữa để bắn
                }
            }
            else
            {
                if (transform.position.x > player.position.x)
                {
                    speed = 2;
                }
                else
                {
                    speed = -2;
                }

            }
        }
        

    }

    private void OnDrawGizmosSelected()//Vẽ
    {
        Gizmos.color = Color.green;//mau xanh
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void TakeDame(float damage)
    {
        //GameObject floatingText = Instantiate(TextPoint, transform.position, Quaternion.identity) as GameObject; //Clone text
        //floatingText.transform.GetChild(0).GetComponent<TextMesh>().text = "" + damage;//Ghi chỉ số cho text
        heart -= damage;
        if (heart <= 0)
        {
            anim.SetBool("Die", true);
        }
    
    }
}
