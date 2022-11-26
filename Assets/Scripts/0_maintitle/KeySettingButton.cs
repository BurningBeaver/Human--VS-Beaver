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
        text = GetComponentInChildren<TextMeshProUGUI>();
        SetText();
    }

    public void SetText()
    {
        text.text = keyCodeStringDict.TryGetValue(KeyCode, out var code) ? code : KeyCode.ToString();
    }
}