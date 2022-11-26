using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndView : MonoBehaviour
{
    public Image fadeImage;

    public void OnSceneMove(string name)
    {
        fadeImage.DOFade(1, 0.5f).From(0).OnStart(() => { fadeImage.gameObject.SetActive(true); })
            .OnComplete(() => { SceneManager.LoadScene(name); });
    }
}