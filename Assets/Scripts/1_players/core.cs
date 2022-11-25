using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sameMove : MonoBehaviour
{
    //[SerializeField]
    //GameObject interactingObject;
    public KeyCode interAct, up, down, left, right;
    [SerializeField]
    float mSpeed;
    bool keyItemCount;//, gageCount;
    bool over;

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
        if (!over)
            transform.Translate(new Vector2(x * mSpeed, y * mSpeed));
    }

    public void InterActCheck()
    {
        if (Input.GetKeyDown(interAct))
        {

        }
    }
    public void InteractEnd()
    {
        keyItemCount = false;
    }

    public void itemGet()
    {
        keyItemCount = true;
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