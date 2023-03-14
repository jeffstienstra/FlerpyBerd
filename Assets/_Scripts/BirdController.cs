using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class BirdController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text flapCountText;

    public LogicManagerScript logic;
    public BirdSoundController birdSoundController;

    public Rigidbody2D myRigidbody;
    public bool birdIsAlive = true;
    public int flapCount = 0;
    public int _flapStrength = 27;
    public float highestPitch = 1.1f;
    public float lowestPitch = 0.9f;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        birdSoundController = GameObject.FindGameObjectWithTag("BirdSounds").GetComponent<BirdSoundController>();

        logic.togglePauseGame();
    }

    void Update()
    {
        flapCountText.text = flapCount.ToString();

        // shield effect logic
        if (logic.shieldIsActive)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        // unpause game
        if (Input.GetKeyDown(KeyCode.Space) == true && logic.isPaused)
        {
            logic.togglePauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true && !logic.isPaused)
        {
            logic.togglePauseGame();
        }

        // gameOver if player off screen
        if (transform.position.y > 18 || transform.position.y < -18)
        {
            SetGameOver();
        }

        // flap
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            if (birdSoundController.birdFlap.isPlaying)
            {
                flapCount += 1;
                birdSoundController.birdFlap.pitch += .2f;
                birdSoundController.birdFlap.Play();

                if (flapCount < 3)
                {
                    flapCount += 1;
                }
                else if (flapCount == 3)
                {
                    logic.increaseSquawkMeter();
                    flapCount += 1;
                }
            }
            else
            {
                flapCount = 0;
                float randomValue = Random.Range(lowestPitch, highestPitch);
                birdSoundController.birdFlap.pitch = randomValue;
                birdSoundController.birdFlap.Play();
                //logic.resetSquawkMeter();
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
            birdSoundController.getEgg.Play();
        }
    }

    void SetGameOver()
    {
        if (birdIsAlive)
        {
            birdSoundController.hit1.Play();
            birdSoundController.hit2.Play();
            birdSoundController.squawk.Play();
        }

        logic.gameOver();
        birdIsAlive = false;
    }
}
