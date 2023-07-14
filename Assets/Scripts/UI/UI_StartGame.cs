using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartGame : MonoBehaviour
{
    public Action OnStartGame = null;

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        button.onClick.AddListener(StartGame);
    }
    private void OnDestroy()
    {
        OnStartGame = null;
        button.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        OnStartGame?.Invoke();
    }
}