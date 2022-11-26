using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    float tiemSpan, currentTime;
    [SerializeField]
    house[] houses;
    private void Start()
    {
        currentTime = tiemSpan;
    }
    private void Update()
    {
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }


    }
    public void CheckHouses()
    {
        int i = houses.Length;
        foreach (house h in houses)
        {
            if (h.GetStat())
                i--;
        }
        if (i == 0)
            GameOver();   
    }
    public void GameOver()
    {

    }
}
