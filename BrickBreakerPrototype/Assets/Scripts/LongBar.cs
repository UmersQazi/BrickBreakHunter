using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBar : NormalBar
{

    public bool moveRight = true;
    public float enemySpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {

        bulletRb = bullet.GetComponent<Rigidbody2D>();

        //StartCoroutine(bulletInterval());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * enemySpeed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * enemySpeed, 0, 0);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Move Trigger"))
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
    }


}
