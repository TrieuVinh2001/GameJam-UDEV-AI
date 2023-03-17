using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float heart;
    public float speed;
    private float speedStart;
    private bool isAttack;
    private bool isMove;
    public float damage;
    public float attackRange;
    public float attackWaitTime;
    private float nextAttackTime = 1f;
    public LayerMask playerLayers;
    public Transform attackPoint;//Vị trí tấn công
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speedStart = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (heart <= 0)
        {
            anim.SetBool("Die", true);
            Destroy(gameObject,2f);//Hủy quái
        }

        if (isAttack)
        {
            StartCoroutine(EnemyAttack());
        }
    }
    
    public void TakeDame(float damage)
    {
        heart -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        House house = collision.GetComponent<House>();
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("House"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("House"))
        {
            speed = speedStart;
            isAttack = false;
        }
    }

    IEnumerator AttackTime()
    {
        while (true)
        {
            anim.SetTrigger("Attacked");
            isAttack = false;
            yield return new WaitForSeconds(attackWaitTime);
            anim.ResetTrigger("Attacked");
            isAttack = true;
        }
        
    }

    IEnumerator EnemyAttack()//Tấn công của enemy
    {
        anim.SetBool("Run", false);
        speed = 0;
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        if (players.Length <= 0)
        {
            isAttack = false;
            speed = speedStart;
            anim.SetBool("Run", true);
        }
        else
        {
            Player health = players[0].GetComponent<Player>();
            House house = players[0].GetComponent<House>();
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackWaitTime;
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(0.5f);

                if (health != null)
                {  
                    health.TakeDamage(damage);//Gây damage
                }
                if(house != null)
                {
                    house.TakeDamage(damage);
                }
                //players[0].GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
