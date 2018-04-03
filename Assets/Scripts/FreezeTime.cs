using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Freeze());
    }

    IEnumerator Freeze()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;

    }

}
