using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    void Start()
    {
        var seq = DOTween.Sequence();

        seq.Append(GetComponent<RectTransform>().DOLocalMoveY(30, 0.1f).SetRelative().SetLoops(-1, LoopType.Yoyo));
    }

    public void A()
    {
        for (int i = 0; i < 3; i++)
        {
            var o = gameObject;
            var a = Instantiate(o, o.transform.parent);
            var pos = a.GetComponent<RectTransform>().localPosition;
            pos += (Vector3)(Random.insideUnitCircle * 500);

            a.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
            a.GetComponent<RectTransform>().localPosition = pos;
        }
    }
}