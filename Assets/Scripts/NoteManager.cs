using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] GameObject spawnBall;
    [SerializeField] float interval;
    [SerializeField] Vector2Int minMaxSpawnsPerInterval;
    [SerializeField] Transform[] possiblePositions;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;

            for (int i = 0; i < GetRandomSpawnCount(); i++)
            {
                Instantiate(spawnBall, GetRandomSpawnPos(), Quaternion.identity);
            }
        }
    }

    private int GetRandomSpawnCount()
    {
        return Random.Range(minMaxSpawnsPerInterval.x, minMaxSpawnsPerInterval.y);
    }

    private Vector3 GetRandomSpawnPos()
    {
        return possiblePositions[Random.Range(0, possiblePositions.Length)].transform.position;
    }
}
