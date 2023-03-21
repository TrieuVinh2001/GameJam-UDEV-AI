using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;

    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);//Hủy sau 2 giây
    }

    private void Update()
    {
        //dùng để flip (chuyển hướng về player)
        Vector3 rotation = transform.eulerAngles;
        transform.eulerAngles = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("House") || collision.gameObject.CompareTag("WeaponFactory") || collision.gameObject.CompareTag("BulletFactory"))
        {
            Player health = collision.GetComponent<Player>();
            House house = collision.GetComponent<House>();
            WeaponFactory weaponFactory = collision.GetComponent<WeaponFactory>();
            BulletFactory bulletFactory = collision.GetComponent<BulletFactory>();
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
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
