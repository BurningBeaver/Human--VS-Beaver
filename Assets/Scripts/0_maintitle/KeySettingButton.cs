using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeySettingButton : MonoBehaviour
{
    [field: SerializeField]
    public PlayerInput InputAction { get; set; }
    [field:SerializeField]
    public KeyCode KeyCode { get; set; }

    private TextMeshProUGUI text;
    private Graphic graphic;
    public Button Button { get; private set; }

    private readonly Dictionary<KeyCode, string> keyCodeStringDict = new()
    {
        {KeyCode.LeftArrow, "←"},
        {KeyCode.UpArrow, "↑"},
        {KeyCode.RightArrow, "→"},
        {KeyCode.DownArrow, "↓"},
        {KeyCode.RightShift, "RS"},
        {KeyCode.RightControl, "RC"},
    };

    private void Awake()
    {
        Button = GetComponent<Button>();
        graphic = GetComponent<Graphic>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        SetText();
    }

    private void Update()
    {
        var trueWhite = Color.white;
        var falseWhite = trueWhite;
        falseWhite.a = 0.5f;
        graphic.color = Input.GetKey(KeyCode) ? trueWhite : falseWhite;
    }

    public void SetText()
    {
        text.text = keyCodeStringDict.TryGetValue(KeyCode, out var code) ? code : KeyCode.ToString();
    }
}