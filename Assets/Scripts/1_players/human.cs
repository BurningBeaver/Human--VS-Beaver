using UnityEngine;

public class Human : Core
{
    private static readonly int IsPickOnWater = Animator.StringToHash("is_pick_on_water");

    
    [SerializeField] int gageCount, goalGage, WDH;

    protected override void InterActCheck()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            if (!isInteracting)
            {
                WDH = GetTile();

                if (WDH != 0)
                    isInteracting = true;
            }
            else
            {
                if (WDH != GetTile())
                {
                    Debug.Log(1);

                    isInteracting = false;
                    gageCount = 0;
                }

                gageUp();
            }
        }
    }

    private int GetTile()
    {
        if (waterManager.IsWater(transform.position)&&!GetKeyItem())
        {
            return 1;
        }

        if (waterManager.IsDam(transform.position))
        {
            return 2;
        }

        //if(매니저에서 집에 닿았는지 확인)
        //return 3

        return 0;
    }

    private void gageUp()
    {
        gageCount++;
        if (goalGage <= gageCount)
        {
            if (WDH == 1)
            {
                Debug.Log("물 획득");
                animator.SetBool(IsPickOnWater, true);
                itemGet();
                InteractEnd();
            }
            else if (WDH == 2)
                destroying();
            else
                pouring();
        }
    }

    private void destroying()
    {
        waterManager.RemoveDam(transform.position);
        InteractEnd();
    }

    private void pouring()
    {
        //지정된 집의 gagedown
        animator.SetBool(IsPickOnWater, false);
        InteractEnd();
    }
}