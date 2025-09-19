using UnityEngine;

public class Movement2 : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    public float moveDistance = 1.25f;
    void Update()
    {
        //moves up and down
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector2.up);
            if (animator != null)
            {
                animator.SetTrigger("MoveTrigger"); //play jump animation
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {

            Move(Vector2.down);
            if (animator != null)
            {
                animator.SetTrigger("MoveTrigger");
            }
        }
        else
        {
            animator.ResetTrigger("MoveTrigger");
        }
    }

    void Move(Vector2 direction)
    {
    transform.position += (Vector3)(direction * moveDistance);
    }
}