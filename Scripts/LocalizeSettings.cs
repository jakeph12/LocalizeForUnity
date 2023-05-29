using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

[CreateAssetMenu(fileName = "New Localize File", menuName = "Setting/New Localize File")]
public class LocalizeSettings : ScriptableObject
{
    public List<Languages> m_lLanguage = new List< Languages>();
}

[System.Serializable]
public class Languages
{
    public string NameLanguage; 
    public List<Word> AllWards = new List<Word>();
}
[System.Serializable]

public class Word
{
    public string Key, Value;
    public Word(string key, string value)
    {
        Key = key;
        Value = value;
    }
}