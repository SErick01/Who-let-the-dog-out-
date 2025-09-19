using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //one has to have rigidbody and collision box is not trigger (player)
    // other is trigger; rigidbody??? cars
    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            animator.SetTrigger("CrashTrigger");
        }
    }
}
