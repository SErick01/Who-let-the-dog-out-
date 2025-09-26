using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on player GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Move the player upwards
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // Trigger the "Up" animation
            if (animator != null)
            {
                animator.SetTrigger("MoveTrigger"); // Ensure this trigger exists in your Animator
            }
        }
        else
        {
            animator.ResetTrigger("MoveTrigger");
        }
    }
}
