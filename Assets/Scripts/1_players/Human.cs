using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Human : Core
{
    private static readonly int IsPickOnWater = Animator.StringToHash("is_pick_on_water");
    [Header("범위")] [SerializeField] private float bucketRange;
    [SerializeField] int gageCount, goalGage, WDH;

    protected override void InterActCheck()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            if (!isInteracting)
            {
                WDH = GetTile();
                Debug.Log(WDH);
                if (WDH != 0)
                    isInteracting = true;
            }
            else
            {
                gageUp();

                if (WDH != GetTile())
                {
                    Debug.Log(1);

                    isInteracting = false;
                    gageCount = 0;
                }
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
            var houses = Physics2D.OverlapCircleAll(transform.position, bucketRange).Where(p => p.CompareTag("House"))
                .ToArray();

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
            SoundManager.Instance.PlaySFX("WaterEmpty_" + Random.Range(0, 2));
            isInteracting = false;
            gageCount = 0;
        }
        else if (goalGage <= gageCount)
        {
            isInteracting = false;
            gageCount = 0;
            if (WDH == 1)
            {
                SoundManager.Instance.PlaySFX("WaterFill");
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
        var houses = Physics2D.OverlapCircleAll(transform.position, bucketRange).Where(p => p.CompareTag("House"))
            .ToArray();
        foreach (var house in houses)
        {
            house.GetComponent<house>().GageUp();
        }

        animator.SetBool(IsPickOnWater, false);
        InteractEnd();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bucketRange);
    }
#endif
}