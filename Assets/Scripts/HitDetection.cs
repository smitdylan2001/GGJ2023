using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CheckObject(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckObject(other.transform);
    }

    private void CheckObject(Transform colliderTransform)
    {
        var angle = Quaternion.Angle(colliderTransform.rotation, transform.rotation);
        if (angle < NoteManager.NoteInfo.MaxAngle)
        {
            NoteManager.Instance.ReturnObject(colliderTransform.gameObject);
        }
    }
}
