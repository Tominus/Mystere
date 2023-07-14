using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_WordManager : MonoBehaviour
{
    [SerializeField] List<W_WordStocker> wordStockers = new List<W_WordStocker>();

    public string GetRandomWord(Enum_JetonType _jetonType)
    {
        int _count = wordStockers.Count;
        for (int i = 0; i < _count; ++i)
        {
            W_WordStocker _wordStocker = wordStockers[i];
            if (_wordStocker.JetonType == _jetonType)
            {
                return _wordStocker.GetRandomWord();
            }
        }

        return "NULL";
    }
}