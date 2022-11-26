using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    private Graphic graphic;
    private float time;

    private void Awake()
    {
        graphic = GetComponent<Graphic>();
    }

    private void Update()
    {
        if (time < 0.5f)
        {
            graphic.color = new Color(1,1,1,1-time);
        }
        else
        {
            graphic.color = new Color(1,1,1,time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
