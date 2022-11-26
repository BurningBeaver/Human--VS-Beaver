using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float timer;
    private IEnumerator Start()
    {
        Sequence a = DOTween.Sequence();
        a.Append(transform.DOMoveY(0.5f, 0.5f).SetRelative());
        a.Join(transform.DOScale(1, 0.5f).From(0.25f));
        a.Join(transform.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).From(0));
        a.Play();

        yield return new WaitForSeconds(timer);
        transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}
