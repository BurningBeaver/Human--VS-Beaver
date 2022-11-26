using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTitleView : MonoBehaviour
{
    public GameObject Option;
    public Image fadeImage;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void StartGame()
    {
        fadeImage.DOFade(1, 0.5f).From(0)
            .OnStart(() => { fadeImage.gameObject.SetActive(true); })
            .OnComplete(() => { SceneManager.LoadScene("2_Game"); });
    }

    public void OptionOn()
    {
        Option.SetActive(true);
    }

    public void OptionOff()
    {
        Option.SetActive(false);
    }
}