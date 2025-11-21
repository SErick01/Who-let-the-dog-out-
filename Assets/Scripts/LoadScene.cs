using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : NonInstantiatingSingleton<LoadScene>
{
    protected override void OnAwake()
    {
        base.OnAwake();

        DontDestroyOnLoad(gameObject);
    }

    protected override LoadScene GetInstance()
    {
        return this;
    }

    /// <summary>
    /// Restarts current scene. 
    /// </summary>
    public void RestartScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadSceneByIndex(index);
    }

    /// <summary>
    /// Works if start scene is 0. 
    /// </summary>
    public void ReloadToStartScene()
    {
        LoadSceneByIndex(0);
    }

    /// <summary>
    /// Loads a scene by its build name. 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    /// <summary>
    /// Loads a scene by its build index. 
    /// </summary>
    /// <param name="index"></param>
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Loads next scene in build order. If it exists. 
    /// </summary>
    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index == 15)
        {
            index += 1;
        }
        if (SceneManager.GetSceneByBuildIndex(index) != null)
        {
            LoadSceneByIndex(index);
        }
    }

    /// <summary>
    /// Quits progam. 
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void WaitToLoadNextScene(float time)
    {
        SetWaitForAction(time, () =>
        {
            LoadNextScene();
        });
    }

    public void WaitToLoadIndex(float time, int index)
    {
        SetWaitForAction(time, () =>
        {
            LoadSceneByIndex(index);
        });
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
