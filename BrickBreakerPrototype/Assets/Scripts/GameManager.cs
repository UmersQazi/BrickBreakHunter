using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI stopWatchText;
    public TextMeshProUGUI ballReturnTimerText;
    public TextMeshProUGUI keyNoticeText;

    
    public TextMeshProUGUI highScoreText;
    public GameObject MatchSetText;
    public bool canPlay;
    public GameObject retryButton;
    public GameObject helpButton;
    public GameObject quitButton;
    public GameObject EventSyst;
    public GameObject ball;
    public float ballReturnTimer = 5;
    public GameObject heart1, heart2, heart3, key;
    
    public Ball ballScript;
    public PlayerController playerScript;
    public float cooldown = 0;
    
    public int enemyCount = 20;
    
    public static float highScore = 1000000.2f;

    [Range(1, 10)]
    public float powerUpcoolDown;
    public bool canPower;
    public bool enemyExist;
    public bool gameOver;

    public float coroutineTime = 0f;
    public float invokeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        canPlay = true;
        ballScript = GameObject.Find("Ball").GetComponent<Ball>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        StartCoroutine(timeFunction());
        //InvokeRepeating("timeIncrease", 1, 1);

    }

    public void timeIncrease()
    {
        invokeTime++;
    }

    IEnumerator timeFunction() {
        while (canPlay)
        {
            yield return new WaitForSeconds(1);
            coroutineTime++;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        enemyExist = GameObject.Find("BarSkull");
        if (!enemyExist || enemyCount == 0)
            gameOver = true;
        if (gameOver)
        {
            canPlay = false;
            
            
            enemyCountText.SetText("Enemies: " + 0);
            MatchSetText.SetActive(true);
            if (playerScript.hasKey)
            {
                
                keyNoticeText.SetText("Got the key! Time -5!");
            }
            if (coroutineTime < highScore)
                highScore = coroutineTime;
                
            highScoreText.SetText("High Score: " + System.Math.Round(highScore) + " seconds");

            EventSyst.SetActive(true);
            retryButton.SetActive(true);
            helpButton.SetActive(true);
            quitButton.SetActive(true);

            
        }


        if (canPlay)
        {
            
            
            string time = "Time: " + coroutineTime + "s";
            
            stopWatchText.SetText(time);
            
            enemyCountText.SetText("Enemies: " + enemyCount);

            if (ballScript.ballLife > 0 && ballScript.ballLife < 2)
            {
                heart1.SetActive(false);
            }
            else if (ballScript.ballLife > 1 && ballScript.ballLife < 3)
            {
                heart2.SetActive(false);
            }
            else if (ballScript.ballLife >= 3)
            {
                heart3.SetActive(false);
                ball.SetActive(false);
                StartCoroutine(ballCooldown());

                ballReturnTimer -= Time.deltaTime;
                //System.Math.Round(ballReturnTimer);

                ballReturnTimerText.SetText("Ball returns in: " + System.Math.Round(ballReturnTimer));
                if (ballReturnTimer < 0)
                {
                    ballReturnTimer = 0;
                    ballReturnTimerText.SetText("");
                }





            }
            else if (ballScript.ballLife == 0)
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                ballReturnTimer = 3;

            }



            

            if (!canPower)
            {
                StartCoroutine(playerCooldown());
            }

        }
        if (playerScript.hasKey)
        {
            key.SetActive(true);
            
            
            //StartCoroutine(keyNotice());
            



        }

    }

    public void Retry()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Help()
    {
        SceneManager.LoadScene("Help");
    }

    IEnumerator keyNotice()
    {
        yield return new WaitForSeconds(3);
        keyNoticeText.SetText("");
    }

    IEnumerator ballTimerDecrease()
    {
        yield return new WaitForSeconds(1);
        ballReturnTimer--;
    }


    IEnumerator ballCooldown()
    {
        

        yield return new WaitForSeconds(cooldown);
        ballScript.ballLife = 0;
        ball.SetActive(true);
        ball.transform.position = ballScript.player.transform.position + ballScript.offset;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        
    }

    IEnumerator slidesPower()
    {
        yield return new WaitForSeconds(3);
        playerScript.Axe.SetActive(false);
        playerScript.Axe2.SetActive(false);
        canPower = false;
    }
    IEnumerator playerCooldown()
    {
        yield return new WaitForSeconds(powerUpcoolDown);
        canPower = true;
    }
}



