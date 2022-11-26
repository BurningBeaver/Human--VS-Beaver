using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color burningColor;
    int Gage, GoalGage;

    public bool onFire
    {
        get => Gage >= GoalGage;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = burningColor;
    }

    public bool GetStat()
    {
        return onFire;
    }

    public void GageUp()
    {
        Gage++;
        Debug.Log("house Poured");
        if (Gage >= GoalGage)
        {
            effect.SetActive(false);
        }
    }
}