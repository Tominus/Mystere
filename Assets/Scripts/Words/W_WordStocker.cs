using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class W_WordStocker
{
    [SerializeField] Enum_JetonType jetonType = Enum_JetonType.Enum_JetonType_MAX;
    [SerializeField] List<string> words = new List<string>();

    public Enum_JetonType JetonType => jetonType;
    public List<string> Words => words;

    public string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}