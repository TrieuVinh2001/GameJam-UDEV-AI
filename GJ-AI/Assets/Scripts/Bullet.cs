using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage;
    public GameObject bulletExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        EnemyBom enemyBom = col.GetComponent<EnemyBom>();
        EnemyFly enemyFly = col.GetComponent<EnemyFly>();
        if (enemy != null || enemyBom !=null || enemyFly !=null)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                enemy.TakeDame(damage);
                Instantiate(bulletExplosion, enemy.transform.position,Quaternion.identity);

            }
            else if (col.gameObject.CompareTag("EnemyFly"))
            {
                enemyFly.TakeDame(damage);
                Instantiate(bulletExplosion, enemyFly.transform.position, Quaternion.identity);
            }
            else if(col.gameObject.CompareTag("EnemyBom"))
            {
                enemyBom.TakeDame(damage);
                Instantiate(bulletExplosion, enemyBom.transform.position, Quaternion.identity);
            }
            
            StartCoroutine(WaitDestroy(0));
        }
        else if (col.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(WaitDestroy(0f));
        }

    }
    IEnumerator WaitDestroy(float timedestroy)
    {
        //move = false;
        yield return new WaitForSeconds(0.03f);
        Destroy(gameObject, timedestroy);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }
}
