using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public LogicManagerScript logic;
    public GameObject pipe;
    public GameObject egg;
    private float timer = 0;

    private BirdController _birdController;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        _birdController = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdController>();
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (_birdController.birdIsAlive)
        {
            if (timer < logic.spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnPipe();
            }
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - logic.heightOffset;
        float highestPoint = transform.position.y + logic.heightOffset;
        float randomValue = Random.Range(lowestPoint, highestPoint);

        Instantiate(pipe, new Vector3(transform.position.x, randomValue, 0), transform.rotation);
        Instantiate(egg, new Vector3(transform.position.x, randomValue, 0), transform.rotation);
        timer = 0;
    }
}
