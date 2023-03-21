using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBom : MonoBehaviour
{
    public float heart;
    public float speed;
    private bool isAttack;
    public float damage;
    public float attackRange;
    public LayerMask playerLayers;
    public Transform attackPoint;//Vị trí tấn công
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (heart <= 0)
        {
            speed = 0;
            anim.SetBool("Die", true);
            Destroy(gameObject, 0.5f);//Hủy quái
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("House") || collision.gameObject.CompareTag("WeaponFactory") || collision.gameObject.CompareTag("BulletFactory"))
        {
            isAttack = true;
        }
    }

    IEnumerator EnemyAttack()//Tấn công của enemy
    {
        speed = 0;
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        if (players.Length >= 0)
        {
            isAttack = false;
            Player health = players[0].GetComponent<Player>();
            House house = players[0].GetComponent<House>();
            WeaponFactory weaponFactory = players[0].GetComponent<WeaponFactory>();
            BulletFactory bulletFactory = players[0].GetComponent<BulletFactory>();
            anim.SetBool("Explosion",true);
            yield return new WaitForSeconds(0.5f); 
            if (health != null)
            {
                health.TakeDamage(damage);//Gây damage
            }
            if (house != null)
            {
                house.TakeDamage(damage);
            }
            if (weaponFactory != null)
            {
                weaponFactory.TakeDamage(damage);
            }
            if (bulletFactory != null)
            {
                bulletFactory.TakeDamage(damage);
            }
            Destroy(gameObject, 1.5f);
        }
    }
}
