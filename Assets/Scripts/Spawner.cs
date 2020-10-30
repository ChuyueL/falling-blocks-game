using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingBlockPrefab;
    public Vector2 secondsBetweenSpawnsMinMax;
    //float secondsBetweenDifficultyIncrease = 5;
    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;

    float nextSpawnTime;
    //float nextDifficultyIncreaseTime;
    float screenHalfWidthInWorldUnits;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        screenHalfWidthInWorldUnits = cam.aspect * cam.orthographicSize;
        height = cam.orthographicSize;
        //nextDifficultyIncreaseTime = Time.time + secondsBetweenDifficultyIncrease;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Time.time > nextDifficultyIncreaseTime && secondsBetweenSpawns >= 0.5f)
        {
            nextDifficultyIncreaseTime = Time.time + secondsBetweenDifficultyIncrease;
            secondsBetweenSpawns -= 0.1f;
        }
        */

        if (Time.time > nextSpawnTime)
        {
            //linear interpolation between min and max spawn times
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            //need to add falling block's height to spawn height so blocks spawn above screen
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax); //Rotation is around z-axis

            Vector2 randomSpawnPosition = new Vector2(Random.Range(-screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits), height + spawnSize);

            GameObject newBlock = Instantiate(fallingBlockPrefab, randomSpawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newBlock.transform.localScale = Vector3.one * spawnSize;
            
        }
        
    }
}
