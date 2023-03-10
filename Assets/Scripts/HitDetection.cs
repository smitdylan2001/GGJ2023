using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField] GameObject goodVFX, perfectVFX;

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
            var score = Mathf.Clamp((int)(Mathf.InverseLerp(NoteManager.NoteInfo.MaxAngle, 0, angle) * 100), 10, 100);
            var note = colliderTransform.GetComponentInParent<Note>();
            if (note.IsRight) NoteManager.Instance.ReturnObjectR(note.gameObject, score);
            else NoteManager.Instance.ReturnObjectL(note.gameObject, score);

            if (score > NoteManager.NoteInfo.MaxAngle / 2)
            {
                Instantiate(perfectVFX, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(goodVFX, transform.position, transform.rotation);
            }
        }
    }
}
