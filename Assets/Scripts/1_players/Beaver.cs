
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Beaver : Core
{
    [SerializeField]
    int gageCount, goalGage;
    [SerializeField]
    float handLength;
    [SerializeField] int keyItemCount, maxItemCount;
    public GameObject damCreateEffect;

    protected override void Update()
    {
        base.Update();
        if (KeyItemNotFULL())
        {
            print("itemgettinhg");
            var woods = Physics2D.OverlapCircleAll(transform.position, handLength).Where(p => p.CompareTag("Wood"));
            if (woods != null)
                foreach (var w in woods)
                {
                    w.gameObject.SetActive(false);
                    itemGet();
                }
        }
    }

    protected override void InterActCheck()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            if (!isInteracting && waterManager.IsWater(transform.position) && !waterManager.IsDam(transform.position) && KeyItemHave())
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
        animator.SetTrigger("action");
        if (goalGage <= gageCount)
            Setting();
    }
    private void Setting()
    {
        Instantiate(damCreateEffect, transform.position, Quaternion.identity);

        gageCount = 0;
        SoundManager.Instance.PlaySFX("DamCreate");
        waterManager.SetDam(transform.position);
        InteractEnd();
    }
    public override void InteractEnd()
    {
        base.InteractEnd();
        UseItem();
    }
    public override void itemGet()
    {
        keyItemCount++;
        animator.SetInteger("item_count", keyItemCount);
    }
    public void UseItem()
    {
        keyItemCount--;
        animator.SetInteger("item_count", keyItemCount);
    }
    public bool KeyItemHave()
    {
        if (keyItemCount > 0)
            return true;
        return false;
    }
    public bool KeyItemNotFULL()
    {
        if (keyItemCount <= maxItemCount)
            return true;
        return false;
    }
}
