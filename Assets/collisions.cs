using UnityEngine;

public class collisions : MonoBehaviour
{
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
            soundtrack.Stop();
            gameOver.Play();
        }
    }

}
