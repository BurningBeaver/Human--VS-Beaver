using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerInput
{
    MoveUp,
    MoveLeft,
    MoveDown,
    MoveRight,
    Action
}

public class InputManager : MonoBehaviour
{
    private readonly Dictionary<PlayerInput, KeyCode> player1Keys = new()
    {
        {PlayerInput.MoveUp, KeyCode.W},
        {PlayerInput.MoveLeft, KeyCode.A},
        {PlayerInput.MoveDown, KeyCode.S},
        {PlayerInput.MoveRight, KeyCode.D},
        {PlayerInput.Action, KeyCode.F},
    };

    private readonly Dictionary<PlayerInput, KeyCode> player2Keys = new()
    {
        {PlayerInput.MoveUp, KeyCode.UpArrow},
        {PlayerInput.MoveLeft, KeyCode.LeftArrow},
        {PlayerInput.MoveDown, KeyCode.DownArrow},
        {PlayerInput.MoveRight, KeyCode.RightArrow},
        {PlayerInput.Action, KeyCode.RightShift},
    };

    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var gameObject = new GameObject("InputManager");
                DontDestroyOnLoad(gameObject);

                _instance = gameObject.AddComponent<InputManager>();
            }

            return _instance;
        }
    }

    public KeyCode GetPlayer1InputCode(PlayerInput input) => player1Keys[input];
    public KeyCode GetPlayer2InputCode(PlayerInput input) => player2Keys[input];
    
    public void OnClickSetKeyButton(PlayerInput input, bool isPlayer1, Action<KeyCode> onComplete = null)
    {
        StartCoroutine(SetKey(key =>
        {
            var targetPlayer = isPlayer1 ? player1Keys : player2Keys;
            targetPlayer[input] = key;
            onComplete?.Invoke(key);
        }));
    }

    private IEnumerator SetKey(Action<KeyCode> onGetKeyDown)
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (var code in Enum.GetValues(typeof(KeyCode)).OfType<KeyCode>())
        {
            if (Input.GetKeyDown(code))
            {
                onGetKeyDown(code);
            }
        }
    }
}
