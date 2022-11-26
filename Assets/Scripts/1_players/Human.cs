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
    [SerializeField] private int waterCount;
    private TileInteraction WDH;
    private bool cantInteractWithRiver;
    public GameObject waterEffect;
    public GameObject damRemoveEffect;

    private void Start()
    {
        upKey = InputManager.Instance.GetPlayer1InputCode(PlayerInput.MoveUp);
        leftKey = InputManager.Instance.GetPlayer1InputCode(PlayerInput.MoveLeft);
        downKey = InputManager.Instance.GetPlayer1InputCode(PlayerInput.MoveDown);
        rightKey = InputManager.Instance.GetPlayer1InputCode(PlayerInput.MoveRight);
        interActionKey = InputManager.Instance.GetPlayer1InputCode(PlayerInput.Action);
    }

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
                {
                    isInteracting = true;
                    switch (WDH)
                    {
                        case TileInteraction.WaterGetting:
                            break;
                        case TileInteraction.DamDestroying:
                            break;
                        case TileInteraction.Pouring:
                            break;
                    }
                }
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
        if (WDH == TileInteraction.DamDestroying)
        {
            animator.SetTrigger("Destroy");
        }

        if (WDH == TileInteraction.WaterGetting)
        {
            SoundManager.Instance.PlaySFX("WaterFill");
            animator.SetTrigger("Bucket");
        }

        if (WDH == TileInteraction.Pouring)
        {
            Pouring();
            SoundManager.Instance.PlaySFX("WaterEmpty_" + Random.Range(0, 2));
            isInteracting = false;
            gageCount = 0;
        }

        if (waterCount <= gageCount && WDH == TileInteraction.WaterGetting)
        {
            isInteracting = false;
            gageCount = 0;

            Instantiate(waterEffect, transform.position, Quaternion.identity);

            Debug.Log("물 획득");
            animator.SetBool(IsPickOnWater, true);
            itemGet();
            InteractEnd();
        }

        if (goalGage <= gageCount && WDH == TileInteraction.DamDestroying)
        {
            isInteracting = false;
            gageCount = 0;
            Destroying();
        }
    }

    private void Destroying()
    {
        Instantiate(damRemoveEffect, transform.position, Quaternion.identity);
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

        animator.SetTrigger("Bucket");
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