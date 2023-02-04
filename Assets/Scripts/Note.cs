using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool IsRight;
    float timer;
    Collider collider;
    
    private void OnEnable()
    {
        timer = 0;
        collider.enabled = false;
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!collider.enabled && timer > 0.7f)
        {
            timer = 0;
        }
        else if(timer > NoteManager.NoteInfo.SpawnInterval*2) 
        {
            if(IsRight) NoteManager.Instance.ReturnObjectR(gameObject);
            else NoteManager.Instance.ReturnObjectL(gameObject);
        }
    }
}
