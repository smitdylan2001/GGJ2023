using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool IsRight;
    float timer;
    [SerializeField] Collider col;

    ParticleSystem particles;
    
    private void OnEnable()
    {
        timer = 0;
        col.enabled = false;
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!col.enabled && timer > 0.7f)
        {
            Debug.Log("enable col");
            timer = 0;
        }
        else if(timer > NoteManager.NoteInfo.SpawnInterval*2)
        {
            Debug.Log("Destroy");
            if(IsRight) NoteManager.Instance.ReturnObjectR(gameObject);
            else NoteManager.Instance.ReturnObjectL(gameObject);
        }
    }
}
