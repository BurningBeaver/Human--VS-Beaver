using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private void Awake()
    {
        Sequence a = DOTween.Sequence();
        a.Append(transform.DOMoveY(0.5f, 0.5f).SetRelative());
        a.Join(transform.DOScale(1, 0.5f).From(0));
        a.Join(transform.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).From(0));
        a.Play();
    }
}
