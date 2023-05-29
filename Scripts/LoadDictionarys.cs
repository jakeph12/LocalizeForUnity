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
    private static Dictionary<string, string> m_dcAllWord = new Dictionary<string, string>();
    private static Lagnguage m_enSavCh;
    public static Lagnguage m_enChangebl
    {
        get=> m_enSavCh;
        set
        {
            m_enSavCh = value;
            PlayerPrefs.SetInt("Language", (int)m_enSavCh);
        }
    }
    public static bool m_bInit;

    private static bool Init()
    {
        LocalizeSettings lcst = Resources.Load<LocalizeSettings>("localize");
        if (!lcst)
        {
            Debug.LogError("You dont have localize File");
            return false;
        }
        m_enSavCh = (Lagnguage)PlayerPrefs.GetInt("Language");
        if(lcst.m_lLanguage.Count > 0)
            foreach(Word lg in lcst.m_lLanguage[(int)m_enChangebl].AllWards)
            {
                m_dcAllWord.Add(lg.Key, lg.Value);
            }
        m_bInit = true;
        return true;
    }
    public static string GetValue(string Key)
    {
        if (!Init()) return Key;
        string value;
        m_dcAllWord.TryGetValue(Key,out value);
        value ??= Key;

        
        return value;
    }
}
