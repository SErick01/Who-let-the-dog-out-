using UnityEngine;

public class collisions : MonoBehaviour
{
    public static bool winGame = false;
    public static bool GameOver = false;

    private Animator animator;
    public AudioSource soundtrack;
    public AudioSource gameOver;
    public AudioSource victory_continue;
    public AudioSource crash;

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
            gameOver.Play();
            soundtrack.Stop();
            crash.Play();
        }
        else if (collision.CompareTag("Cat"))
        {
            winGame = true;
            animator.SetBool("winGame", true);
            soundtrack.Stop();
            victory_continue.Play();
        }
    }

}
