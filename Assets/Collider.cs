using UnityEngine;

//non-trigger; physics based?
public class Collider : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            animator.SetTrigger("CrashTrigger");
        }
    }
}
