using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public struct NoteInformation
{
    public float SpawnInterval;
    public float IndicatorDelay;
    public float MaxAngle;
    public float RequiredPoints;
}

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }
    public static NoteInformation NoteInfo { get; private set; }

    [HideInInspector] public List<GameObject> AvailableSpawnsR = new List<GameObject>();
    [HideInInspector] public List<GameObject> AvailableSpawnsL = new List<GameObject>();

    [SerializeField] MoveTimeline timelines;
    [SerializeField] NoteInformation noteInfo;
    [SerializeField] GameObject[] possibleSpawnsR;
    [SerializeField] GameObject[] possibleSpawnsL;
    [SerializeField] GameObject[] allScenes;
    [SerializeField] Animator[] animators;
    [SerializeField] Transform rightCurtain, leftCurtain;

    int currentScene;
    int currentBeat;
    float timer;
    bool canPlay = true;
    WaitForSeconds waitBeat;

    private void Awake()
    {
        Instance = this;
        NoteInfo = noteInfo;

        AvailableSpawnsR = possibleSpawnsR.ToList();
        foreach (GameObject go in possibleSpawnsR) go.SetActive(false);

        AvailableSpawnsL = possibleSpawnsL.ToList();
        foreach (GameObject go in possibleSpawnsL) go.SetActive(false);

        waitBeat = new WaitForSeconds(NoteInfo.SpawnInterval);
    }

    void Update()
    {
        if (!canPlay) return;

        timer += Time.deltaTime;
        if (timer >= NoteInfo.SpawnInterval)
        {
            timer = 0;

            var slice = timelines.Slices[currentScene].TimeSlices[currentBeat];

            var item = slice.SpawnPositionR;
            var rot = slice.RotationR;

            if(item) SpawnObjectR(item, Quaternion.Euler(rot));

            item = slice.SpawnPositionL;
            rot = slice.RotationL;

            if (item) SpawnObjectL(item, Quaternion.Euler(rot));

            currentBeat = (currentBeat + 1) % timelines.Slices[currentScene].TimeSlices.Length;
        }
    }

    public void GoToNextScene()
    {
        StartCoroutine(SceneSwitch());
    }

    IEnumerator SceneSwitch()
    {
        canPlay = false;

        foreach (Animator ani in animators)
        {
            if (ani.gameObject) ani.SetTrigger("NextState");
        }
        yield return waitBeat;
        yield return waitBeat;

        float elapsedTime = 0;
        var time = NoteInfo.SpawnInterval * 2;
        //Move curtuns
        while (elapsedTime < time)
        {
            var lstart = leftCurtain.position;
            var rstart = rightCurtain.position;
            var offset = new Vector3(2, 0, 0);
            leftCurtain.position = Vector3.Lerp(lstart, lstart - (offset*-1), (elapsedTime / time));
            rightCurtain.position = Vector3.Lerp(rstart, rstart - (offset*1), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //switch states
        allScenes[currentScene % allScenes.Length].SetActive(false);
        currentScene = (currentScene + 1) % timelines.Slices.Length;
        currentBeat = 0;
        foreach (Animator ani in animators)
        {
            ani.SetTrigger("StartState");
        }
        allScenes[currentScene % allScenes.Length].SetActive(true);

        elapsedTime = 0;
        //Move curtuns
        while (elapsedTime < time)
        {
            var lstart = leftCurtain.position;
            var rstart = rightCurtain.position;
            var offset = new Vector3(2, 0, 0);
            leftCurtain.position = Vector3.Lerp(lstart, lstart - (offset * 1), (elapsedTime / time));
            rightCurtain.position = Vector3.Lerp(rstart, rstart - (offset * -1), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canPlay = true;
    }

    private void SpawnObjectR(GameObject obj, Quaternion rot)
    {
        obj.SetActive(true);
        obj.transform.rotation = rot;
        AvailableSpawnsR.Remove(obj);
    }

    private void SpawnObjectL(GameObject obj, Quaternion rot)
    {
        obj.SetActive(true);
       obj.transform.rotation = rot;
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
