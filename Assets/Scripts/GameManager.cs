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

        NoteManager.Instance.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(time);

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        NoteManager.Instance.enabled = false;
        endScreen.SetActive(true);
    }
}