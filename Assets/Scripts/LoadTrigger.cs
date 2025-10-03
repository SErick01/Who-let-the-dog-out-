using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public bool loadsNextScene;

    public int sceneIndexToLoad;

    public float WaitTime = 3f;


    void Start()
    {
        Load();
    }

    public void Load()
    {
        if (loadsNextScene)
        {
            LoadScene.Instance.WaitToLoadNextScene(WaitTime);
        }
        else
        {
            LoadScene.Instance.WaitToLoadIndex(WaitTime, sceneIndexToLoad);
        }
    }
}
