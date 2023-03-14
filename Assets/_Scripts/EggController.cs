using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public LogicManagerScript logic;
    private BirdController _birdController;

    public float deadZone = -45;


    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        _birdController = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdController>();

    }
    void Update()
    {
        transform.position += (Vector3.left * logic.moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 && !logic.gameOverScreen.activeSelf)
        {
            logic.addScore(1);
        }

        if (other.gameObject.CompareTag("Bird") && _birdController.birdIsAlive)
        {
            Destroy(gameObject);
        }
    }
}
