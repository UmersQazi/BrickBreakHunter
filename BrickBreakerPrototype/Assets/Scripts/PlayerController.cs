using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 5;
    public float jumpInput;
    public bool isOnGround;
    public bool hasKey;

    public Ball ballScript;
    public GameManager gameManagerScript;

    
    public GameObject Axe;
    public GameObject Axe2;
    public bool hasAxe;

    public GameObject effect;
    public AudioSource enemyBlast;

    [Range(1,100)] public float jumpVelocity;

    PlayerControl controls;

    // Start is called before the first frame update
    void Awake()
    {
        hasKey = false;
        ballScript = GameObject.Find("Ball").GetComponent<Ball>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        controls = new PlayerControl();
        controls.Gameplay.Move.performed += ctx => ControllerMovement();
        controls.Gameplay.Jump.performed += ctx => ControllerJump();
        controls.Gameplay.Call.performed += ctx => ballScript.ControllerCall();
        
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManagerScript.canPlay)
        {
            

            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
                isOnGround = false;
            }

            if (Input.GetKeyDown(KeyCode.W) && gameManagerScript.canPower)
            {
                Axe.SetActive(true);
                Axe2.SetActive(true);
                hasAxe = true;

            }
        }
        if (!gameManagerScript.canPlay)
            controls.Disable();

    }


    void ControllerMovement()
    {
        


    }

    void ControllerJump()
    {
        if (isOnGround)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            isOnGround = false;
        }
    }




    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {

            enemyBlast = gameObject.GetComponent<AudioSource>();
            enemyBlast.Play();
            Destroy(collision.gameObject);
            gameManagerScript.enemyCount--;
            GameObject spark = Instantiate(effect, transform.position, Quaternion.identity);
            spark.GetComponent<ParticleSystem>().Play();
        }else if (collision.gameObject.CompareTag("Key"))
        {
            gameManagerScript.coroutineTime -= 5;
            Destroy(collision.gameObject);
            hasKey = true;
        }
    }




}
