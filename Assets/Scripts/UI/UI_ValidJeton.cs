using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ValidJeton : MonoBehaviour
{
    public Action OnValidJetonRead = null;

    [SerializeField] UI_Jeton validJetonCopy;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI validJetonWord;
    [SerializeField] RectTransform validJetonPosition;

    private void Start()
    {
        button.onClick.AddListener(ValidJetonRead);
    }
    private void OnDestroy()
    {
        OnValidJetonRead = null;
        button.onClick.RemoveAllListeners();
    }
    
    public void CopieJeton(UI_Jeton _jetonToCopie, string _validWord)
    {
        Transform buttonTransform = button.transform;
        validJetonCopy = Instantiate(_jetonToCopie, buttonTransform);
        validJetonCopy.transform.position = validJetonPosition.position;
        validJetonWord.text = _validWord;

        validJetonCopy.gameObject.SetActive(true);
        validJetonCopy.UnactivateButton();
    }
    public void ResetValidJeton()
    {
        validJetonWord.text = "";
        if (validJetonCopy)
            Destroy(validJetonCopy.gameObject);
    }

    private void ValidJetonRead()
    {
        OnValidJetonRead?.Invoke();
        Destroy(validJetonCopy.gameObject);
    }
}