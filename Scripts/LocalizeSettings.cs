using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Localize File", menuName = "Setting/New Localize File")]
public class LocalizeSettings : ScriptableObject
{
    public List<Languages> m_lLanguage = new List<Languages>();
}

[System.Serializable]
public class Languages
{
    public string m_strNameLanguage; 
    public List<Word> m_lisAllWards = new List<Word>();
}
[System.Serializable]

public class Word
{
    public string m_strKey, m_strValue;
    public Word(string key, string value)
    {
        m_strKey = key;
        m_strValue = value;
    }
}
