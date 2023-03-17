using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float heart;
    public float speed;
    Rigidbody2D rb;
    private Animator ani;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heart <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(1f, 0f, 0f) * -speed * Time.deltaTime;
            //ani.SetTrigger("Run");
            ani.SetBool("Run", true);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weapon.transform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
            //ani.SetTrigger("Run");
            ani.SetBool("Run", true);
            transform.localScale = new Vector3(1f, 1f, 1f);
            weapon.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            ani.SetBool("Run", false);
        }
    }
    
    public void TakeDamage(float damage)
    {
        heart -= damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        House house = col.GetComponent<House>();
        WeaponFactory weaponFactory = col.GetComponent<WeaponFactory>();
        if (col.gameObject.CompareTag("House"))
        {
            house.btnUpdate.gameObject.SetActive(true);//Hiện nút Update
        }
        else if (col.gameObject.CompareTag("WeaponFactory"))
        {
            weaponFactory.btnUpdate.gameObject.SetActive(true);//Hiện nút Update
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        House house = col.GetComponent<House>();
        WeaponFactory weaponFactory = col.GetComponent<WeaponFactory>();
        if (col.gameObject.CompareTag("House"))
        {
            house.btnUpdate.gameObject.SetActive(false);//Ẩn nút Update
        }
        else if (col.gameObject.CompareTag("WeaponFactory"))
        {
            weaponFactory.btnUpdate.gameObject.SetActive(false);//Ẩn nút Update
        }
    }
}
