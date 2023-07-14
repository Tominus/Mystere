using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Jeton : MonoBehaviour
{
    public Action<UI_Jeton> OnButtonPush;

    [SerializeField] Image validity;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Enum_JetonType type = Enum_JetonType.Enum_JetonType_MAX;

    RectTransform buttonTransform;

    public Rect GetJetonSize => buttonTransform.rect;
    public Enum_JetonType Type => type;

    public void SetJetonData(Sprite _sprite, string _text, Enum_JetonType _type)
    {
        text.text = _text;
        button.image.sprite = _sprite;
        type = _type;
    }
    public void SetJetonValidity(Sprite _sprite)
    {
        validity.sprite = _sprite;
        validity.color = new Color(1, 1, 1, 1);
    }

    private void Start()
    {
        buttonTransform = (RectTransform)button.transform;
        button.onClick.AddListener(ButtonPush);
    }
    private void OnDestroy()
    {
        OnButtonPush = null;
        button.onClick.RemoveAllListeners();
    }

    public void UnactivateButton()
    {
        button.enabled = false;
    }

    private void ButtonPush()
    {
        OnButtonPush?.Invoke(this);
    }
}