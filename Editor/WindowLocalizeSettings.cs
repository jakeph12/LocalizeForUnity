using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class WindowLocalizeSettings : EditorWindow
{
    public static LocalizeSettings main;
    private string NameNewLanguage;
    private bool Open,AddNewLan;
    private Vector2 scrollPosition;
    [MenuItem("Window/Localize/Settings")]
    public static void Showwin()
    {
        if (!main) main = Resources.Load<LocalizeSettings>("localize");
        if (!main)
        {
            Debug.LogError("First you need to create localize file in folder Resources");
            return;
        }
         GetWindow<WindowLocalizeSettings>("Localize Settings");
    }
    public void OnGUI()
    {
        if (main)
        {
            if (GUILayout.Button("Add Language")) AddNewLan = !AddNewLan;
            if (AddNewLan)
            {
                NameNewLanguage = EditorGUILayout.TextField("Language name", NameNewLanguage);
                if(GUILayout.Button("Add new language"))
                {
                    if(main.m_lLanguage.Count > 0)
                    {
                        Languages lg = new Languages();
                        lg.NameLanguage = NameNewLanguage;
                        if(main.m_lLanguage[0].AllWards.Count > 0)
                            foreach (var m in main.m_lLanguage[0].AllWards)
                            {
                                lg.AllWards.Add(new Word(m.Key,m.Value));
                            }
                        main.m_lLanguage.Add(lg);
                    }
                    else
                    {
                        Languages lg = new Languages();
                        lg.NameLanguage = NameNewLanguage;
                        lg.AllWards.Add(new Word("New key", "New value"));
                        main.m_lLanguage.Add(lg);
                    }
                }
                return;
            }

            if (Open)
            {
                if (main.m_lLanguage.Count > 0)
                        DrowAll();

            }
            else
            {
                if (GUILayout.Button("ExportLoc")) ExportTranslate();
                if (GUILayout.Button("ImportLoc")) ImoprtTranslate();
            }
            if (GUILayout.Button("V")) Open = !Open;
            if (GUI.changed)
            {
                EditorUtility.SetDirty(main);
                EditorSceneManager.MarkAllScenesDirty();
            }

        }
        else
        {
            if (GUILayout.Button("Reload File"))
            {
                if (!main) main = Resources.Load<LocalizeSettings>("localize");
                if (!main)
                {
                    Debug.LogError("First you need to create localize file in folder Resources");
                    return;
                }
            }
        }      
    }
    public void DrowAll()
    {
        LocalizeSettings Mai = main;
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Key", GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < Mai.m_lLanguage.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField(Mai.m_lLanguage[i].NameLanguage, GUILayout.Width(50));
            if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(25)))
            {
                if (Mai.m_lLanguage.Count > 0)
                    Mai.m_lLanguage.Remove(Mai.m_lLanguage[i]);
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndHorizontal();
        for (int a = 0; a < Mai.m_lLanguage[0].AllWards.Count; a++)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(25)))
            {
                for (int i = 0; i < Mai.m_lLanguage.Count; i++) Mai.m_lLanguage[i].AllWards.RemoveAt(a);
                return;
            }

            string NewS = EditorGUILayout.TextField(Mai.m_lLanguage[0].AllWards[a].Key);
            for (int i = 0; i < Mai.m_lLanguage.Count; i++)
            {
                Mai.m_lLanguage[i].AllWards[a].Key = NewS;
                Mai.m_lLanguage[i].AllWards[a].Value = EditorGUILayout.TextField(Mai.m_lLanguage[i].AllWards[a].Value);
            }
            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("add new word"))
        {
            foreach (var K in Mai.m_lLanguage)
            {
                K.AllWards.Add(new Word("New Key", "New word"));
            }
        }

    }
    public void ExportTranslate()
    {
        var b = new BinaryFormatter();

        var w = File.OpenWrite("./Assets/Resources/Exportloc.inf");

        b.Serialize(w, main.m_lLanguage);
        w.Close();
    }
    public void ImoprtTranslate()
    {
        var b = new BinaryFormatter();

        var w = File.OpenRead("./Assets/Resources/Exportloc.inf");

        var data = b.Deserialize(w);
        w.Close();
        main.m_lLanguage.AddRange((List<Languages>)data);
    }
}
