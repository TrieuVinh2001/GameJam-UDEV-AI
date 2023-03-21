using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float heart;
    private float maxHeart;
    public float speed;

    public Image hpImage;

    public LayerMask playerLayers;
    Rigidbody2D rb;
    private Animator ani;
    public GameObject weapon;
    private bool isCheckHouse;
    private bool isCheckBulletFactory;

    // Start is called before the first frame update
    void Start()
    {
        maxHeart = heart;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heart > maxHeart)
        {
            heart = maxHeart;
        }
        hpImage.fillAmount = heart / maxHeart;
        if (heart <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.A) && transform.position.x > -25)
        {
            transform.position += new Vector3(1f, 0f, 0f) * -speed * Time.deltaTime;
            //ani.SetTrigger("Run");
            ani.SetBool("Run", true);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weapon.transform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < 25)
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

        if (isCheckHouse)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                StartCoroutine(UpHouse());
            }
        }

        if (isCheckBulletFactory)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(BuyBullet());
            }
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
        BulletFactory bulletFactory = col.GetComponent<BulletFactory>();
        if (col.gameObject.CompareTag("House"))
        {
            house.btnUpdate.gameObject.SetActive(true);//Hiện nút Update
            isCheckHouse = true;
        }
        else if (col.gameObject.CompareTag("WeaponFactory"))
        {
            weaponFactory.btnUpdate.gameObject.SetActive(true);//Hiện nút Update
            isCheckHouse = true;
        }
        else if (col.gameObject.CompareTag("BulletFactory"))
        {
            bulletFactory.btnBuy.gameObject.SetActive(true);//Hiện nút Update
            isCheckBulletFactory = true;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        House house = col.GetComponent<House>();
        WeaponFactory weaponFactory = col.GetComponent<WeaponFactory>();
        BulletFactory bulletFactory = col.GetComponent<BulletFactory>();
        if (col.gameObject.CompareTag("House"))
        {
            isCheckHouse = false;
            house.btnUpdate.gameObject.SetActive(false);//Ẩn nút Update
        }
        else if (col.gameObject.CompareTag("WeaponFactory"))
        {
            isCheckHouse = false;
            weaponFactory.btnUpdate.gameObject.SetActive(false);//Ẩn nút Update
        }
        else if (col.gameObject.CompareTag("BulletFactory"))
        {
            isCheckBulletFactory = false;
            bulletFactory.btnBuy.gameObject.SetActive(false);//Ẩn nút Update
        }
    }

    IEnumerator UpHouse()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 1f, playerLayers);
        House house = players[0].GetComponent<House>();
        WeaponFactory weaponFactory = players[0].GetComponent<WeaponFactory>();
        yield return new WaitForSeconds(0.1f);
        if (house != null)
        {
            house.BtnUpdate();
        }
        if (weaponFactory != null)
        {
            weaponFactory.BtnUpdate();
        }
    }

    IEnumerator BuyBullet()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 1f, playerLayers);
        BulletFactory bulletFactory = players[0].GetComponent<BulletFactory>();
        yield return new WaitForSeconds(0.1f);
        if (bulletFactory != null)
        {
            bulletFactory.BtnBuy();
        }
    }
}
