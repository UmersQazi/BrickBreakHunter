using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBar : MonoBehaviour
{

    public GameObject bullet;
    public Rigidbody2D bulletRb;
    public float speed = 20f;
    private Vector3 offset = new Vector3(0, -1, 0);



    // Start is called before the first frame update
    void Start()
    {
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        StartCoroutine(bulletInterval());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator bulletInterval()
    {

        while (true)
        {
            yield return new WaitForSeconds(2);
            Shoot();
        }
        

    }


    public void Shoot()
    {
        Instantiate(bullet, transform.position + offset, transform.rotation);
        bulletRb.AddForce(Vector2.down * speed * Time.deltaTime);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy Trigger"))
        {
            transform.Translate(2, 0, 0);
        }
    }

}
