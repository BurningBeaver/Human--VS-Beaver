using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using Random = UnityEngine.Random;

public class Human : Core
{
    private enum TileInteraction
    {
        WaterGetting = 1,
        DamDestroying,
        Pouring
    }

    private bool keyItemHave;
    private static readonly int IsPickOnWater = Animator.StringToHash("is_pick_on_water");
    [Header("범위")] [SerializeField] private float bucketRange;
    [SerializeField] int gageCount, goalGage;
    private TileInteraction WDH;
    private bool cantInteractWithRiver;
    public GameObject waterEffect;

    private IEnumerator BreakWait()
    {
        cantInteractWithRiver = true;
        yield return new WaitForSeconds(.5f);
        cantInteractWithRiver = false;
    }

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
                if (GetTile() == TileInteraction.DamDestroying && keyItemHave)
                {
                    return;
                }

                GageUp();
                if (WDH != GetTile())
                {
                    isInteracting = false;
                    gageCount = 0;
                }
            }
        }
    }

    private TileInteraction GetTile()
    {
        if (waterManager.IsWater(transform.position) && !keyItemHave && !cantInteractWithRiver)
        {
            return TileInteraction.WaterGetting;
        }

        if (waterManager.IsDam(transform.position) && !cantInteractWithRiver && !keyItemHave)
        {
            return TileInteraction.DamDestroying;
        }

        if (keyItemHave)
        {
            var houses = Physics2D.OverlapCircleAll(transform.position, bucketRange).Where(p => p.CompareTag("House"))
                .ToArray();

            if (houses.Length > 0)
                return TileInteraction.Pouring;
        }

        return 0;
    }

    private void GageUp()
    {
        gageCount++;
        if (WDH == TileInteraction.Pouring)
        {
            Pouring();
            SoundManager.Instance.PlaySFX("WaterEmpty_" + Random.Range(0, 2));
            isInteracting = false;
            gageCount = 0;
        }
        else if (goalGage <= gageCount)
        {
            isInteracting = false;
            gageCount = 0;
            if (WDH == TileInteraction.WaterGetting)
            {
                Instantiate(waterEffect, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX("WaterFill");
                Debug.Log("물 획득");
                animator.SetBool(IsPickOnWater, true);
                itemGet();
                InteractEnd();
            }
            else if (WDH == TileInteraction.DamDestroying)
            {
                Destroying();
            }
        }
    }

    private void Destroying()
    {
        waterManager.RemoveDam(transform.position);
        InteractEnd();
        StartCoroutine(BreakWait());
    }

    private void Pouring()
    {
        var houses = Physics2D.OverlapCircleAll(transform.position, bucketRange).Where(p => p.CompareTag("House"))
            .ToArray();
        foreach (var house in houses)
        {
            house.GetComponent<house>().GageUp();
        }

        animator.SetBool(IsPickOnWater, false);
        InteractEnd();
        UseItem();
    }

    public override void itemGet()
    {
        keyItemHave = true;
    }

    public void UseItem()
    {
        keyItemHave = false;
    }


    /* public override void InteractEnd()
     {
         base.InteractEnd();
         WDH = 0;
     }*/

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bucketRange);
    }
#endif
}