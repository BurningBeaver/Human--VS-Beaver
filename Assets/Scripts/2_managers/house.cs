using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    int Gage, GoalGage;
    bool onFire;
    public bool GetStat()
    {
        return onFire;
    }
    public void GageUp()
    {
        Gage++;
        if (Gage >= GoalGage)
            onFire = false;
    }
}
