using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHold : MonoBehaviour
{
    [SerializeField] private float timeRequired = 2;
    float timer;
    [SerializeField] UnityEvent holdEvent;
    [SerializeField] RectTransform rectTransform;
    float scale;
    private void Start()
    {
        scale = rectTransform.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        rectTransform.localScale = new Vector3(0, rectTransform.localScale.y, rectTransform.localScale.z);
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        rectTransform.localScale = new Vector3(Mathf.Lerp(0,scale,timer/timeRequired), rectTransform.localScale.y, rectTransform.localScale.z);

        if (timer > timeRequired)
        {
            holdEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rectTransform.localScale = new Vector3(scale, rectTransform.localScale.y, rectTransform.localScale.z);
    }
}
