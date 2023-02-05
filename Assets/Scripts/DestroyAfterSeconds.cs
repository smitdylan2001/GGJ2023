using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float time;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
