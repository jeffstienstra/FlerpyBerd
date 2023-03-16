using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class BirdController : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private TMPro.TMP_Text flapCountText;

    public Rigidbody2D myRigidbody;
    public bool birdIsAlive = true;
    public int flapCount = 0;
    public int _flapStrength = 27;
    public float highestPitch = 1.1f;
    public float lowestPitch = 0.9f;


    void Start()
    {
        gameManager.togglePauseGame();
    }

    void Update()
    {
        flapCountText.text = flapCount.ToString();

        // shield effect logic
        if (gameManager.shieldIsActive)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        // unpause game
        if (Input.GetKeyDown(KeyCode.Space) == true && gameManager.isPaused)
        {
            gameManager.togglePauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true && !gameManager.isPaused)
        {
            gameManager.togglePauseGame();
        }

        // gameOver if player off screen
        if (transform.position.y > 18 || transform.position.y < -18)
        {
            SetGameOver();
        }

        // flap
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            if (GameInstance.Instance.birdFlap.isPlaying)
            {
                flapCount += 1;
                GameInstance.Instance.birdFlap.pitch += .2f;
                GameInstance.Instance.birdFlap.Play();

                if (flapCount < 3)
                {
                    flapCount += 1;
                }
                else if (flapCount == 3)
                {
                    gameManager.increaseSquawkMeter();
                    flapCount += 1;
                }
            }
            else
            {
                flapCount = 0;
                float randomValue = Random.Range(lowestPitch, highestPitch);
                GameInstance.Instance.birdFlap.pitch = randomValue;
                GameInstance.Instance.birdFlap.Play();
                //gameManager.resetSquawkMeter();
            }

            myRigidbody.velocity = Vector2.up * _flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetGameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Egg") && birdIsAlive)
        {
            GameInstance.Instance.getEgg.Play();
        }
    }

    void SetGameOver()
    {
        if (birdIsAlive)
        {
            GameInstance.Instance.hit1.Play();
            GameInstance.Instance.hit2.Play();
            GameInstance.Instance.squawk.Play();
        }

        gameManager.gameOver();
        birdIsAlive = false;
    }
}
