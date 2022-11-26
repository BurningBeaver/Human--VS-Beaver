using UnityEngine;
using System.Linq;
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
        if (waterManager.IsWater(transform.position) && !GetKeyItem())
        {
            return 1;
        }

        if (waterManager.IsDam(transform.position))
        {
            return 2;
        }

        if (GetKeyItem())
        {
            var houses = Physics2D.OverlapCircleAll(transform.position, 5).Where(p => p.CompareTag("House")).ToArray();

            if (houses.Length > 0)
                return 3;
        }
        return 0;
    }

    private void gageUp()
    {
        gageCount++;
        if (WDH == 3)
        {
            pouring();
            gageCount = 0;
        }
        else if (goalGage <= gageCount)
        {
            gageCount = 0;
            if (WDH == 1)
            {
                Debug.Log("물 획득");
                animator.SetBool(IsPickOnWater, true);
                itemGet();
                InteractEnd();
            }
            else if (WDH == 2)
                destroying();
        }
    }

    private void destroying()
    {
        waterManager.RemoveDam(transform.position);
        InteractEnd();
    }

    private void pouring()
    {
        var houses = Physics2D.OverlapCircleAll(transform.position, 5).Where(p => p.CompareTag("House")).ToArray();
        foreach (var house in houses)
        {
            house.GetComponent<house>().GageUp();
        }
        animator.SetBool(IsPickOnWater, false);
        InteractEnd();
    }
}