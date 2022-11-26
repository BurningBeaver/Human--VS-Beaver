using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] GameObject branch;

    private void Start()
    {
        generate();
    }

    public void generate()
    {
        float x = Random.Range(.5f, 3.7f);
        float y = Random.Range(.5f, 3.7f);
        Vector2 spot = Random.insideUnitCircle * 5f;

        var mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(new Vector3(1, 1.25f, 1), 0.15f));
        mySequence.Append(transform.DOScale(new Vector3(1.25f, 0.75f, 1), 0.15f));
        mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.15f));
        mySequence.OnComplete(() =>
        {
            var transformPosition = transform.position + (Vector3)spot;
            var a =Instantiate(branch, transform.position, transform.rotation);
            a.transform.DOMove(transformPosition, 0.4f).SetEase(Ease.OutCubic);
        });
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime + Random.Range(0.0f, 1.5f));
        generate();
    }
}