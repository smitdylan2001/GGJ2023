using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool IsRight;
    float timer;
    [SerializeField] Collider col;

    ParticleSystem[] particles;

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        timer = 0;
        col.enabled = false;
        foreach(var particle in particles)
        {
            particle.Play();
        }
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!col.enabled && timer > NoteManager.NoteInfo.SpawnInterval * 0.8)
        {
            Debug.Log("enable col", gameObject);
            col.enabled = true;
            timer = 0;
        }
        else if(timer > NoteManager.NoteInfo.SpawnInterval*1.2)
        {
            Debug.Log("Destroy", gameObject);
            if(IsRight) NoteManager.Instance.ReturnObjectR(gameObject);
            else NoteManager.Instance.ReturnObjectL(gameObject);
        }
    }
}
