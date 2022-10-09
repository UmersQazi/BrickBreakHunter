using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Range(1, 200)] public float velocityS;

    [Range(1, 300)] public float fixedSpeed;

    public float topBound = 2.94f;
    public float bottomBound = -3f;
    public float leftBound = -13f;
    public float rightBound = 13f;


    public float ballLife = 0;

    public Rigidbody2D ballRb;
    public GameObject player;
    public Rigidbody2D playerRb;
    public Vector3 offset = new Vector3 (0, 2, 0);

    public bool canCall;

    public AudioSource bounceSound;
    GameManager gameMan;


    PlayerControl controls;

    // Start is called before the first frame update
    void Start()
    {
        canCall = false;
        controls = new PlayerControl();
        controls.Gameplay.Call.performed += ctx => ControllerCall();

        gameMan = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        ballRb = GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(velocityS, velocityS));
        
        
    }

    



    // Update is called once per frame
    void FixedUpdate()
    {
        ballRb.velocity = ballRb.velocity.normalized * fixedSpeed;


        if (gameMan.canPlay)
        {

            if (Input.GetKey(KeyCode.E))
            {
                ballRb.AddForce((player.transform.position - transform.position).normalized * fixedSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetButton("Fire1"))
            {
                ballRb.AddForce((player.transform.position - transform.position).normalized * fixedSpeed, ForceMode2D.Impulse);
            }

            if (ballLife > 3)
            {
                gameObject.SetActive(false);

            }
        }

    }

    public void ControllerCall()
    {
        
           
            
        
        


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Player"))
        {
            ballRb.AddForce(new Vector2(velocityS, velocityS));
        }


        //Replacing the ball's position if it gets offscreen
        if(collision.gameObject.CompareTag("Ball Trigger"))
        {
            transform.position = player.transform.position + offset;
        }

       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ballRb.AddForce(new Vector2(velocityS, velocityS + 200));
            ballLife = 0;
            ballRb.velocity += new Vector2(1, 0);
            bounceSound = gameObject.GetComponent<AudioSource>();
            bounceSound.Play();

        }
        if (collision.gameObject.CompareTag("Player")&&Input.GetButton("Fire2"))
        {
            ballRb.velocity = new Vector2(0, 0);
            playerRb.velocity = new Vector2(0, 0);
            

        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ballLife++;
            bounceSound = gameObject.GetComponent<AudioSource>();
            bounceSound.Play();

        }
        
    }

}
