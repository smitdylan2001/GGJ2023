using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject endScreen, startScreen;

    public void StartGame()
    {
        startScreen.SetActive(false);
        StartCoroutine(PlayGame());
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PlayGame()
    {
        var time = audioSource.clip.length;
        NoteManager.Instance.StartCoroutine(NoteManager.Instance.OpenCurtains());
        NoteManager.Instance.canPlay = true;
        audioSource.Play();
        yield return new WaitForSeconds(time);

        NoteManager.Instance.canPlay = false;
        NoteManager.Instance.StopAllCoroutines();
        NoteManager.Instance.StartCoroutine(NoteManager.Instance.CloseCurtains());
        ScoreManager.Instance.UpdateEndText();

        endScreen.SetActive(true);
    }
}