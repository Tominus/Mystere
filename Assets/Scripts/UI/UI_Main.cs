using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    [SerializeField] RectTransform plateau;
    [SerializeField] UI_StartGame uiStartGame;
    [SerializeField] UI_ResetGame uiResetGame;
    [SerializeField] UI_ValidJeton uiValidJeton;
    [SerializeField] W_WordManager wordManager;

    [SerializeField] UI_Jeton jetonPrefab;
    [SerializeField] Vector2Int jetonGridSpacing;
    [SerializeField] List<UI_Jeton> jetonList;
    [SerializeField] List<Sprite> jetonSprites;
    [SerializeField] Sprite validJetonSprite;
    [SerializeField] Sprite invalidJetonSprite;

    [SerializeField] UI_Jeton validJeton;
    [SerializeField] string validWord;

    private void Start()
    {
        uiResetGame.OnResetGame += ResetGame;
        StartGame();
    }

    private void StartGame()
    {
        ShowStartGame();
    }
    private void ResetGame()
    {
        validJeton = null;
        validWord = "";

        uiValidJeton.ResetValidJeton();

        uiValidJeton.OnValidJetonRead -= ShowPlateau;
        uiStartGame.OnStartGame -= ShowValidJetonAndWord;

        uiStartGame.gameObject.SetActive(false);
        uiValidJeton.gameObject.SetActive(false);

        int _count = jetonList.Count;
        for (int i = 0; i < _count; ++i)
            Destroy(jetonList[i].gameObject);
        jetonList.Clear();

        StartGame();
    }

    private void InitializeJetonList()
    {
        List<int> _tmpJetonTypeList = new List<int>();
        int _max = (int)Enum_JetonType.Enum_JetonType_MAX;
        for (int i = 0; i < _max; ++i)
        {
            _tmpJetonTypeList.Add(i);
        }

        Vector3 _position = plateau.position;
        _position.x -= jetonGridSpacing.x;
        _position.y += jetonGridSpacing.y;

        int _count = 0;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                UI_Jeton _jeton = Instantiate(jetonPrefab, _position, Quaternion.identity, plateau);

                int _random = Random.Range(0, _max - _count);
                Enum_JetonType _jetonType = (Enum_JetonType)_tmpJetonTypeList[_random];
                _tmpJetonTypeList.RemoveAt(_random);

                _jeton.SetJetonData(jetonSprites[(int)_jetonType], _jetonType.ToString(), _jetonType);

                jetonList.Add(_jeton);
                _jeton.gameObject.SetActive(false);

                ++_count;
                _position.x += jetonGridSpacing.x;
            }
            _position.x -= jetonGridSpacing.x * 3;
            _position.y -= jetonGridSpacing.y;
        }
    }
    private void InitializeValidJeton()
    {
        validJeton = jetonList[Random.Range(0, 9)];
        validWord = wordManager.GetRandomWord(validJeton.Type);
    }
    private void InitializeButtonEvent()
    {
        int _count = jetonList.Count;
        for (int i = 0; i < _count; ++i)
        {
            UI_Jeton _jeton = jetonList[i];
            _jeton.OnButtonPush += PlayerSelectedJeton;
            _jeton.gameObject.SetActive(true);
        }
    }

    private void ShowStartGame()
    {
        uiStartGame.gameObject.SetActive(true);
        uiStartGame.OnStartGame += ShowValidJetonAndWord;
    }
    private void ShowValidJetonAndWord()
    {
        uiStartGame.gameObject.SetActive(false);
        uiStartGame.OnStartGame -= ShowValidJetonAndWord;

        InitializeJetonList();
        InitializeValidJeton();

        uiValidJeton.gameObject.SetActive(true);
        uiValidJeton.CopieJeton(validJeton, validWord);
        uiValidJeton.OnValidJetonRead += ShowPlateau;
    }
    private void ShowPlateau()
    {
        uiValidJeton.gameObject.SetActive(false);
        uiValidJeton.OnValidJetonRead -= ShowPlateau;

        InitializeButtonEvent();
    }

    private void PlayerSelectedJeton(UI_Jeton _jeton)
    {
        _jeton.OnButtonPush -= PlayerSelectedJeton;

        if (validJeton == _jeton)
        {
            _jeton.SetJetonValidity(validJetonSprite);
        }
        else
        {
            _jeton.SetJetonValidity(invalidJetonSprite);
        }
    }
}