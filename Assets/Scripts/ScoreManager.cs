using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }  

    [SerializeField] private TMP_Text rHandText, lHandText, endText, endText2;
    [SerializeField] private AudioSource cheerAudio;
    [SerializeField] private AudioClip[] cheerClips;
    int multiplier = 1;

    int perfect, good;

    private void Awake()
    {
        Instance= this;
    }

    private void Start()
    {
        Score = 0;
        UpdateScoreText();
    }

    public void AddScore(int addition)
    {
        Debug.Log(addition);
        Score += addition;

        if(addition > NoteManager.NoteInfo.MaxAngle / 2)
        {
            perfect++;
        }
        else
        {
            good++;
        }

        UpdateScoreText();

        if (Score >= 2000 * multiplier)
        {
            NoteManager.Instance.GoToNextScene();
            multiplier++;
        }
        else if(Score >= 1000 * multiplier)
        {
            cheerAudio.clip = cheerClips[Random.Range(0, cheerClips.Length)];
            cheerAudio.Play();
        }
    }

    private void UpdateScoreText()
    {
        var text = Score.ToString();
        rHandText.text = text;
        lHandText.text = text;
    }

    public void UpdateEndText()
    {
        var text = Score.ToString();
        endText.text = "SCORE:\n" + text + "\n\nRESTART";
        endText2.text = "GOOD: " + good.ToString() + " | PERFECT:" + perfect.ToString();
    }
}
