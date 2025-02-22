using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalePortal : MonoBehaviour
{
    public float scaleSpeed = 1f; 

    public void DecreaseScale()
    {
        StartCoroutine(ScaleOverTime(transform.localScale, Vector3.one * 0.1f));
    }

    public void IncreaseScale()
    {
        StartCoroutine(ScaleOverTime(transform.localScale, Vector3.one * 0.3f));
    }

    IEnumerator ScaleOverTime(Vector3 startScale, Vector3 targetScale)
    {
        float elapsedTime = 0;

        while (elapsedTime < scaleSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, (elapsedTime / scaleSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; 
    }
}
