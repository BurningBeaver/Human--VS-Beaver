using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : Core
{
    [SerializeField]
    int gageCount, goalGage;
    // Start is called before the first frame update
   

    protected override void InterActCheck()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            if (!isInteracting && waterManager.IsWater(transform.position) && !waterManager.IsDam(transform.position))
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
        waterManager.SetDam(transform.position);
        InteractEnd();
    }
}
