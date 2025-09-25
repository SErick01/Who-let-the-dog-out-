using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Restarts current scene. 
    /// </summary>
    public static void RestartScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadSceneByIndex(index);
    }

    /// <summary>
    /// Works if start scene is 0. 
    /// </summary>
    public static void ReloadToStartScene()
    {
        LoadSceneByIndex(0);
    }

    /// <summary>
    /// Loads a scene by its build name. 
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    /// <summary>
    /// Loads a scene by its build index. 
    /// </summary>
    /// <param name="index"></param>
    public static void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Loads next scene in build order. If it exists. 
    /// </summary>
    public static void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetSceneAt(index) != null)
        {
            LoadSceneByIndex(index);
        }
    }

    /// <summary>
    /// Quits progam. 
    /// </summary>
    public static void QuitGame()
    {
        Application.Quit();
    }

    public void SetWaitForAction(float time, Action action)
    {
        StartCoroutine(WaitForAction(time, action));
    }

    IEnumerator WaitForAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }
}
