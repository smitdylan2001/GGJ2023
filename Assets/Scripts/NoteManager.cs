using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct NoteInformation
{
    public float SpawnInterval;
    public float IndicatorDelay;
    public float MaxAngle;
}

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }
    public static NoteInformation NoteInfo { get; private set; }

    [HideInInspector] public List<GameObject> AvailableSpawnsR = new List<GameObject>();
    [HideInInspector] public List<GameObject> AvailableSpawnsL = new List<GameObject>();

    [SerializeField] NoteInformation _noteInfo;
    [SerializeField] GameObject[] possibleSpawnsR;
    [SerializeField] GameObject[] possibleSpawnsL;

    float timer;

    private void Awake()
    {
        Instance = this;
        NoteInfo = _noteInfo;
    }

    private void Start()
    {
        AvailableSpawnsR = possibleSpawnsR.ToList();
        foreach(GameObject go in possibleSpawnsR) go.SetActive(false);

        AvailableSpawnsL = possibleSpawnsL.ToList();
        foreach (GameObject go in possibleSpawnsL) go.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= NoteInfo.SpawnInterval)
        {
            timer = 0;

            var item = GetRandomItem(AvailableSpawnsR.Count);
            var rot = Random.rotation;

            if(AvailableSpawnsR.Count>0) SpawnObjectR(AvailableSpawnsR[item], rot);

            item = GetRandomItem(AvailableSpawnsL.Count);
            rot = Random.rotation;

            if (AvailableSpawnsL.Count > 0) SpawnObjectL(AvailableSpawnsL[item], rot);
        }
    }

    private void SpawnObjectR(GameObject obj, Quaternion rot)
    {
        obj.SetActive(true);
        //obj.transform.rotation = rot;
        AvailableSpawnsR.Remove(obj);
    }

    private void SpawnObjectL(GameObject obj, Quaternion rot)
    {
        obj.SetActive(true);
       //obj.transform.rotation = rot;
        AvailableSpawnsL.Remove(obj);
    }

    public void ReturnObjectR(GameObject obj, int score = 0)
    {
        obj.SetActive(false);
        AvailableSpawnsR.Add(obj);

        if (score > 0)
        {
            ScoreManager.Instance.AddScore(score);
        }
    }
    public void ReturnObjectL(GameObject obj, int score = 0)
    {
        obj.SetActive(false);
        AvailableSpawnsL.Add(obj);

        if (score > 0)
        {
            ScoreManager.Instance.AddScore(score);
        }
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
