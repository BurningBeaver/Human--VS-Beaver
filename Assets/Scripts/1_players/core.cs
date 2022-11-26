using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    protected WaterManager waMa;
    //[SerializeField]
    //GameObject interactingObject;
    public KeyCode interAct, up, down, left, right;
    [SerializeField]
    float mSpeed;
    bool keyItemHave, over;
    protected bool isInteracting;//, gageCount;

    public void MoveCheck()
    {
        float x = 0, y = 0;
        if (Input.GetKey(up))
            y = 1;
        if (Input.GetKey(down))
            y = -1;

        if (Input.GetKey(left))
            x = -1;
        if (Input.GetKey(right))
            x = 1;
        if (isInteracting && !over)
            transform.Translate(new Vector2(x * mSpeed, y * mSpeed));
    }

    public virtual void InterActCheck()
    {

    }
    public void InteractEnd()
    {
        isInteracting = false;
        keyItemHave = false;
    }

    public void itemGet()
    {
        keyItemHave = true;
    }

    public void GameOver()
    {
        over = true;
    }
    /* public void InteractStart(GameObject inter)
     {
         interactingObject = inter;
         InteractGageUp();
     }
     public void InteractGageUp()
     {
         gageCount++;
         if (gageCount >= 10)
         {
             gageCount = 0;
             interactingObject = null;
             InteractEnd();
         }
     }
     public virtual void InteractEnd()
     {

     }*/
}