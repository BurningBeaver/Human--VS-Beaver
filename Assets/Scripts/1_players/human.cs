using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Core
{
    [SerializeField]
    int gageCount, goalGage, WDH;
    private void Start()
    {
        waMa = FindObjectOfType<WaterManager>();
    }

    public override void InterActCheck()
    {
        if (Input.GetKeyDown(interAct))
        {
            base.InterActCheck();
            if (!isInteracting)
            {
                if (waMa.IsWater(transform.position))
                    WDH = 1;
                else if (waMa.IsDam(transform.position))
                    WDH = 2;
                //else if(���̸� 3)
                else
                    WDH = 0;
                if (WDH != 0)
                    isInteracting = true;
            }
            if (isInteracting)
                gageUp();
        }
    }
    private void gageUp()
    {
        gageCount++;
        if (goalGage <= gageCount)
        {
            if (WDH == 1)
            {
                itemGet();
                InteractEnd();
            }
            else if (WDH == 2)
                destroying();
            else
                Setting();
        }
    }
    private void destroying()
    {
        waMa.RemoveDam(transform.position);
        InteractEnd();
    }
    private void Setting()
    {
        waMa.SetDam(transform.position);
        InteractEnd();
    }
}
