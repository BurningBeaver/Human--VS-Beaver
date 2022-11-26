using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<house> _houses;

    private float timer = 10;

    [SerializeField] private float maxTimer = 10;

    [SerializeField] private Text timerText;
    [SerializeField] private Gradient timerColor;
    [SerializeField] private Image timerImage;

    private bool isGamePlay;

    private void Awake()
    {
        isGamePlay = true;
        _houses = FindObjectsOfType<house>().ToList();
    }

    private void LateUpdate()
    {
        if (!isGamePlay) return;

        timer = Mathf.Max(timer - Time.deltaTime, 0);
        var timerImageFillAmount = Mathf.Clamp01(timer / maxTimer);
        timerImage.fillAmount = timerImageFillAmount;
        timerImage.color = timerColor.Evaluate(1 - timerImageFillAmount);
        timerText.text = $"{timer:F2}";
        if (timer <= 0)
        {
            GameEnd(false);
        }
    }

    public void CheckHumanGameWin()
    {
        if (_houses.Count == _houses.Count(p => p.onFire))
        {
            GameEnd(true);
        }
    }

    private void GameEnd(bool isHumanWin)
    {
        isGamePlay = false;
        Debug.Log(isHumanWin);
    }
}