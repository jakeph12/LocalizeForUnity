using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLocalize : MonoBehaviour
{
       public enum TypeOfLocalize
    {
        Word,
        Dialog,
    }
    public string m_strKey;
    public TypeOfLocalize m_enTypeOfLocalize;
    void Start()
    {
        if (m_strKey == null || m_strKey == "" || m_strKey == " ") return;
        if (m_enTypeOfLocalize == TypeOfLocalize.Word) GetComponent<Text>().text = LoadDictionarys.GetValue(m_strKey);
        else TranlateDialog();
    }
    void TranlateDialog()
    {
        string d = LoadDictionarys.GetValue(m_strKey);
        string[] dp = d.Split('|');
        var t = GetComponent<Text>();
        t.text = dp[0];
        for (int i = 1; i < dp.Length; i++)
        {
            t.text += '\n' + dp[i];
        }
    }
}
