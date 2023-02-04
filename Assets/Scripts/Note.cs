using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool IsRight;
    float timer;
    
    private void OnEnable()
    {
        timer = 0;
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > NoteManager.NoteInfo.SpawnInterval*2) 
        {
            if(IsRight) NoteManager.Instance.ReturnObjectR(gameObject);
            else NoteManager.Instance.ReturnObjectL(gameObject);
        }
    }
}
