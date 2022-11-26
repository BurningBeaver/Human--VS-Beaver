using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private void Awake()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(3, 1.5f).SetRelative());
        seq.Join(transform.GetComponent<TextMeshPro>().DOFade(0,1.5f).From(1));
    
    }
}
