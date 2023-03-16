using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private GameInstance _instance = GameInstance.Instance;

    public GameManager gameManager;
    //public LogicManagerScript logic;
    private BirdController _birdController;

    public float deadZone = -45;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _birdController = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdController>();

    }
    void Update()
    {
        //TODO: Edit pipe and egg prefabs to include reference to GameManager object in the scene.
        transform.position += (Vector3.left * gameManager.moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 && !gameManager.gameOverScreen.activeSelf)
        {
            gameManager.addScore(1);
        }

        if (other.gameObject.CompareTag("Bird") && _birdController.birdIsAlive)
        {
            Destroy(gameObject);
        }
    }
}
