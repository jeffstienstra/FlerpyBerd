using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject pipe;
    public GameObject egg;
    private float timer = 0;
    float _spawnRate;
    float _heightOffset;

    private GameInstance _instance = GameInstance.Instance;
    private BirdController _birdController;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _spawnRate = gameManager.spawnRate;
        _heightOffset = gameManager.heightOffset;
        // _spawnRate = _instance.difficultyLevels[_instance.selectedDifficulty]["spawnRate"];
        // _heightOffset = _instance.difficultyLevels[_instance.selectedDifficulty]["heightOffset"];
        _birdController = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdController>();
        spawnPipe();
    }

    void Update()
    {
        if (_birdController.birdIsAlive)
        {
            if (timer < _spawnRate)
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
        float lowestPoint = transform.position.y - _heightOffset;
        float highestPoint = transform.position.y + _heightOffset;
        float randomValue = Random.Range(lowestPoint, highestPoint);

        Instantiate(pipe, new Vector3(transform.position.x, randomValue, 0), transform.rotation);
        Instantiate(egg, new Vector3(transform.position.x, randomValue, 0), transform.rotation);
        timer = 0;
    }
}
