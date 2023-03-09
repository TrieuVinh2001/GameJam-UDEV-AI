using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    private Animator ani;
    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(1f, 0f, 0f) * -speed * Time.deltaTime;
            //ani.SetTrigger("Run");
            ani.SetBool("Run", true);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gun.transform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
            //ani.SetTrigger("Run");
            ani.SetBool("Run", true);
            transform.localScale = new Vector3(1f, 1f, 1f);
            gun.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            ani.SetBool("Run", false);
        }
    }
}
