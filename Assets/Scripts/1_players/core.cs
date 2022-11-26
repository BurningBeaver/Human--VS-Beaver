using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Core : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    protected Animator animator;
    protected WaterManager waterManager;

    [Header("조작키")] [SerializeField] protected KeyCode interActionKey;
    [SerializeField] protected KeyCode upKey;
    [SerializeField] protected KeyCode downKey;
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode rightKey;

    [Header("값")] [SerializeField] protected float moveSpeed;

    [SerializeField] bool keyItemHave, over;
    [SerializeField] protected bool isInteracting = false; //, gageCount;

    private void Awake()
    {
        waterManager = FindObjectOfType<WaterManager>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private Vector2 GetDirection()
    {
        float x = 0, y = 0;
        if (Input.GetKey(upKey))
            y = 1;
        if (Input.GetKey(downKey))
            y = -1;

        if (Input.GetKey(leftKey))
            x = -1;
        if (Input.GetKey(rightKey))
            x = 1;
        return new Vector2(x, y).normalized;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(interActionKey))
        {
            InterActCheck();
        }

        var movePosition = GetDirection() * (moveSpeed * Time.deltaTime);

        //이동 기능
        if (!isInteracting && !over && movePosition.magnitude != 0)
        {
            transform.Translate(movePosition);
            animator.SetBool("is_move", true);
        }
        else
        {
            animator.SetBool("is_move", false);
        }

        //방향 제어
        if (movePosition.x != 0)
        {
            var isRightMove = movePosition.x < 0 ? true : false;
            _spriteRenderer.flipX = isRightMove;
        }
    }

    protected abstract void InterActCheck();

    public virtual void InteractEnd()
    {
        isInteracting = false;
    }

    public void itemGet()
    {
        keyItemHave = true;
    }

    public void GameOver()
    {
        over = true;
    }

    protected bool GetKeyItem()
    {
        //Debug.Log("keyItemHave : " + keyItemHave);
        return keyItemHave;
    }
    protected void useItem()
    {
        keyItemHave = false;
    }
}