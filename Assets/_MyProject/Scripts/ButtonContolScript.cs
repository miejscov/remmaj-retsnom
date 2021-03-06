﻿using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContolScript : MonoBehaviour
{
    public static bool isStarting = false;

    public void Load(string name)
    {
        try
        {
            if (Time.timeScale == 0f)
                Time.timeScale = 1f;
            SceneManager.LoadScene(name);
        }
        catch
        {
            Debug.LogError("Nie udało się załadować sceny: " + name);
        }
    }


    public static void LoadStatic(string name)
    {
        try
        {
            SceneManager.LoadScene(name);
        }
        catch
        {
            Debug.LogError("Nie udało się załadować sceny: " + name);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void PlayGame()
    {
        isStarting = true;
        Load("Scene001");
    }

    public void NewGame()
    {
        isStarting = false;
        Load("Scene001");
    }
}
