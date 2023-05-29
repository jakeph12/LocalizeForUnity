using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLocalize : MonoBehaviour
{
    public string Key;
    void Start()
    {
        LoadDictionarys.m_enChangebl = Lagnguage.Sp;
        Debug.Log(LoadDictionarys.GetValue(Key));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
