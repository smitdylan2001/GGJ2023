using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHold : MonoBehaviour
{
    [SerializeField] private float timeRequired = 2;
    float timer;
    [SerializeField] UnityEvent holdEvent;
    
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if(timer > timeRequired)
        {
            holdEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
