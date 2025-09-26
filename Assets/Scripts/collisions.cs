using UnityEngine;

public class collisions : MonoBehaviour
{
    public static bool winGame = false;
    public static bool GameOver = false;

    private Animator animator;
    public AudioSource soundtrack;
    public AudioSource gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            animator.SetTrigger("CrashTrigger");
            GameOver = true;
            soundtrack.Stop();
            gameOver.Play();

            // add delay to Loading Scene
            LoadScene.Instance.LoadSceneByName("BEnd");
        }
        else if (collision.CompareTag("Cat"))
        {
            winGame = true;
            soundtrack.Stop();

            //add delay to Loading Scene; not immediate
            LoadScene.Instance.LoadSceneByName("GEnd1");
            
        }
    }

}
