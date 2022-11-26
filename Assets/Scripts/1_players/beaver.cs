using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : Core
{
    [SerializeField]
    int gageCount, goalGage;
    // Start is called before the first frame update
    void Start()
    {
        waMa = FindObjectOfType<WaterManager>();
    }

    protected override void InterActCheck()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            if (!isInteracting && waMa.IsDam(transform.position) && waMa.IsDam(transform.position))
            {
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
            Setting();
    }
    private void Setting()
    {
        waMa.SetDam(transform.position);
        InteractEnd();
    }
}
