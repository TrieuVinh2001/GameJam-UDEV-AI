using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float lauchForce;
    public Transform shotPoint;
    private float timeWait;
    public float timeAttack;
    //private Animator anim;
    public Transform character;

    public AudioSource shot;

    void Start()
    {
        //anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 weaponPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - weaponPosition;
        transform.right = direction;

        if (Input.GetMouseButton(0) && timeWait < Time.time && GameManager.instance.bulletCount>0)
        {
           // anim.SetTrigger("Attack");
            timeWait = Time.time + timeAttack;//Thời gian chờ cho lần bắn tiếp
            Shoot();
        }
        if (transform.rotation.z < 0.7f && transform.rotation.z > -0.7f)
        {
            //character.localScale = new Vector3(-0.8f, 0.8f, 1f);
            //gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            //character.localScale = new Vector3(0.8f, 0.8f, 1f);
            //gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);
        }
    }

    private void Shoot()
    {
        shot.Play();
        GameManager.instance.bulletCount -= 1;
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * lauchForce;//Tạo đạn
    }
}
