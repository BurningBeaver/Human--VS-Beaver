using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOLocalMove((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime).SetEase(Ease.InOutSine);
    }
}