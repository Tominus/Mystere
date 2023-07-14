using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResetGame : MonoBehaviour
{
    public Action OnResetGame = null;

    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(ResetGame);
    }
    private void OnDestroy()
    {
        OnResetGame = null;
        button.onClick.RemoveAllListeners();
    }

    private void ResetGame()
    {
        OnResetGame?.Invoke();
    }
}