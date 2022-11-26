using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public float duration = 1f;
    public AnimationCurve curve;

    void Update()
    {
        if (!start)
        {
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        yield return new WaitForSeconds(0.5f);
        start = true;

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
