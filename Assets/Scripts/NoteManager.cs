using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct NoteInformation
{
    public float SpawnInterval;
    public float IndicatorDelay;
    public Vector2Int minMaxSpawnsPerInterval;
    public float MaxAngle;
}

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }
    public static NoteInformation NoteInfo { get; private set; }

    [HideInInspector] public List<GameObject> AvailableSpawns = new List<GameObject>();

    [SerializeField] NoteInformation _noteInfo;
    [SerializeField] GameObject[] possibleSpawns;

    float timer;

    private void Awake()
    {
        Instance = this;
        NoteInfo = _noteInfo;
    }

    private void Start()
    {
        AvailableSpawns = possibleSpawns.ToList();
        foreach(GameObject go in possibleSpawns) go.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= NoteInfo.SpawnInterval)
        {
            timer = 0;

            for (int i = 0; i < GetRandomSpawnCount(NoteInfo.minMaxSpawnsPerInterval); i++)
            {
                var item = GetRandomItem(AvailableSpawns.Count);
                var rot = Random.rotation;

                SpawnObject(AvailableSpawns[item], rot);
            }
        }
    }

    private void SpawnObject(GameObject obj, Quaternion rot)
    {
        obj.SetActive(true);
        obj.transform.rotation = rot;
        AvailableSpawns.Remove(obj);
    }

    public void ReturnObject(GameObject obj, float score = 0)
    {
        obj.SetActive(false);
        NoteManager.Instance.AvailableSpawns.Add(obj);
    }

    private static int GetRandomSpawnCount(Vector2Int range)
    {
        return Random.Range(range.x, range.y);
    }

    private static int GetRandomItem(int max)
    {
        return Random.Range(0, max);
    }
}
