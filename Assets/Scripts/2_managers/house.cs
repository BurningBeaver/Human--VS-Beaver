using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    [SerializeField]
    GameObject effect;
    int Gage, GoalGage;
    bool onFire;
    private void Awake()
    {
        onFire = true;
    }
    public bool GetStat()
    {
        return onFire;
    }
    public void GageUp()
    {
        Gage++;
        if (Gage >= GoalGage)
        {
            effect.SetActive(false);
            onFire = false;
        }
    }
}
