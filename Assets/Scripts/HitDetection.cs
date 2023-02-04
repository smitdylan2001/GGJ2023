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
        var angle = Quaternion.Angle(colliderTransform.parent.rotation, transform.GetChild(0).rotation);
        if (angle < NoteManager.NoteInfo.MaxAngle)
        {
            var score = (int)(Mathf.InverseLerp(NoteManager.NoteInfo.MaxAngle, 0, angle) * 100);

            if (colliderTransform.GetComponentInParent<Note>().IsRight) NoteManager.Instance.ReturnObjectR(colliderTransform.gameObject, score);
            else NoteManager.Instance.ReturnObjectL(colliderTransform.gameObject, score);

        }
    }
}
