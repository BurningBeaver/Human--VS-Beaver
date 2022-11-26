using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Input")]
    [SerializeField] private List<KeySettingButton> player1SettingButtons;
    [SerializeField] private List<KeySettingButton> player2SettingButtons;
    [SerializeField] private GameObject setButtonObject;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.SetMasterVolume(value));
        bgmVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.SetBGMVolume(value));
        sfxVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.SetSFXVolume(value));
        player1SettingButtons.ForEach(setting =>
        {
            setting.Button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("ClickSfx");
                text.text = $"Player 1의 '{setting.InputAction}을 담당할 키를 눌러주세요.'";
                setButtonObject.SetActive(true);
                InputManager.Instance.OnClickSetKeyButton(setting.InputAction, true, result =>
                {
                    setting.KeyCode = result;
                    setButtonObject.SetActive(false);
                    setting.SetText();
                }); 
            });
        });
        player2SettingButtons.ForEach(setting =>
        {
            setting.Button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("ClickSfx");
                text.text = $"Player 2의 '{setting.InputAction}을 담당할 키를 눌러주세요.'";
                setButtonObject.SetActive(true);
                InputManager.Instance.OnClickSetKeyButton(setting.InputAction, false, result =>
                {
                    setting.KeyCode = result;
                    setButtonObject.SetActive(false);
                    setting.SetText();
                }); 
            });
        });
    }
}
