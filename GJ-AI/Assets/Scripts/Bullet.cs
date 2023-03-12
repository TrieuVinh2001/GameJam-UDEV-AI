using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        if (enemy != null)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
            enemy.TakeDame(damage);
            StartCoroutine(WaitDestroy(0));
            }
        }
        else if (col.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(WaitDestroy(2f));
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
