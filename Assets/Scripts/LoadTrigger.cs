using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public bool loadsNextScene;
    public int sceneIndexToLoad;
    public float WaitTime = 3f;
    public bool skipped;

    void Start() {Load();}

    public void Load()
    {
        if (loadsNextScene)
        {LoadScene.Instance.WaitToLoadNextScene(WaitTime);}
        
        else
        {LoadScene.Instance.WaitToLoadIndex(WaitTime, sceneIndexToLoad);}
    }

    void Update()
    {
        if (skipped) {return;}

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            skipped = true;
            LoadScene.Instance.StopAllCoroutines();

            if (loadsNextScene)
            {LoadScene.Instance.LoadNextScene();}
            
            else
            {LoadScene.Instance.LoadSceneByIndex(sceneIndexToLoad);}
        }
    }
}
