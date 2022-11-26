using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human : core
{
    [SerializeField]
    int gageCount, goalGage, WDH;
    // Start is called before the first frame update
    void Start()
    {
        waMa = FindObjectOfType<WaterManager>();
    }

    // Update is called once per frame
    void Update()
    {

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
                //else if(ÁýÀÌ¸é 3)
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
