using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }  

    [SerializeField] private TMP_Text rHandText, lHandText;
    int multiplier = 1;
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
        UpdateScoreText();

        if (Score >= 2000 * multiplier)
        {
            NoteManager.Instance.GoToNextScene();
            multiplier++;
        }
    }

    private void UpdateScoreText()
    {
        var text = Score.ToString();
        rHandText.text = text;
        lHandText.text = text;
    }
}
