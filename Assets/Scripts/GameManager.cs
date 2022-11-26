using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<house> _houses;

    private float timer = 10;

    [SerializeField] private float maxTimer = 10;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI woodCountText;
    [SerializeField] private TextMeshProUGUI fireCountText;

    [SerializeField] private Gradient timerColor;
    [SerializeField] private Image timerImage;
    [SerializeField] private Image fadeImage;
    private bool isGamePlay;

    private void Awake()
    {
        timer = maxTimer;
        isGamePlay = true;
        _houses = FindObjectsOfType<house>().ToList();
        CheckHumanGameWin();
    }

    private void LateUpdate()
    {
        if (!isGamePlay) return;

        timer = Mathf.Max(timer - Time.deltaTime, 0);
        var timerImageFillAmount = Mathf.Clamp01(timer / maxTimer);
        timerImage.fillAmount = timerImageFillAmount;
        timerImage.color = timerColor.Evaluate(1 - timerImageFillAmount);
        timerText.text = $"{Mathf.Floor(timer)}초";
        if (timer <= 0)
        {
            GameEnd(false);
        }
    }

    public void CheckHumanGameWin()
    {
        fireCountText.text = $"x {_houses.Count - _houses.Count(p => p.onFire)}";
        if (_houses.Count == _houses.Count(p => p.onFire))
        {
            GameEnd(true);
        }
    }

    private void GameEnd(bool isHumanWin)
    {
        isGamePlay = false;
        fadeImage.DOFade(1, 1).From(0).OnPlay(() => { fadeImage.gameObject.SetActive(true); }).OnComplete(() =>
        {
            if (isHumanWin)
            {
                SceneManager.LoadScene("3-2_Human Win");
            }
            else
            {
                SceneManager.LoadScene("3-1_Beaver Win");
            }
        });
    }

    public void SetWoodCount(int count, int keyItemCount)
    {
        woodCountText.text = $"{count}/{keyItemCount}";
    }
}