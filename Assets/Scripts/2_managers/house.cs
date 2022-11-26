using DG.Tweening;
using UnityEngine;

public class house : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color burningColor;
    [SerializeField] int Gage, GoalGage;

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

    // ReSharper disable Unity.PerformanceAnalysis
    public void GageUp()
    {
        Gage++;
        var mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(new Vector3(1, 1.25f, 1), 0.15f));
        mySequence.Append(transform.DOScale(new Vector3(1.25f, 0.75f, 1), 0.15f));

        mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.15f));

        Debug.Log("house Poured");
        if (Gage >= GoalGage)
        {
            _spriteRenderer.color = Color.white;
            effect.SetActive(false);
            FindObjectOfType<GameManager>().CheckHumanGameWin();
        }
    }
}