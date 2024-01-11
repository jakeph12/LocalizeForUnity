using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Lagnguage
{
    En,
    Ru,
    Sp
}
public class LoadDictionarys : MonoBehaviour
{
        public const string m_strSaveName = "Language";
    private static Dictionary<string, string> m_dcAllWord = new Dictionary<string, string>();
    private static Lagnguage m_enSavCh;
    public static Lagnguage m_enChangebl
    {
        get=> m_enSavCh;
        set
        {
            m_enSavCh = value;
        }
    }
    private static bool _m_bInit;
    public static bool m_bInit
    {
        get
        {
            if (!_m_bInit) Init();
            return _m_bInit;
        }
        set
        {
            _m_bInit = value;
            if (!_m_bInit) m_dcAllWord.Clear();
        }
    }

    private static bool Init()
    {
        LocalizeSettings lcst = Resources.Load<LocalizeSettings>("localize");
        if (!lcst)
        {
            Debug.LogError("You dont have localize File");
            return false;
        }
        m_enSavCh = (Lagnguage)PlayerPrefs.GetInt(m_strSaveName);
        if (lcst.m_lLanguage.Count > 0)
        {
            m_dcAllWord.Clear();
            foreach(Word lg in lcst.m_lLanguage[(int)m_enChangebl].m_lisAllWards)
            {
                m_dcAllWord.Add(lg.m_strKey, lg.m_strValue);
            }
            m_bInit = true;
        }
        return true;
    }
    public static string GetValue(string Key)
    {
        if (!m_bInit) return Key;
        string value;
        m_dcAllWord.TryGetValue(Key,out value);
        value ??= Key;
        return value;
    }
}
