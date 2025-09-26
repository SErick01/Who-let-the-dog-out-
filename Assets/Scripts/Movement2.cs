using NUnit.Framework;
using UnityEngine;
//using System.Collections.Generic;

public class Movement2 : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveDistance = 1.25f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    //private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb; //For physics interactions
    public AudioSource Bork;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("winGame", false);
    }

    // Update is called once per frame

   
    void Update()
    {
        if (!collisions.winGame && !collisions.GameOver)
        {
            //moves up and down
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger"); // Ensure this trigger exists in your Animator
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {

                Move(Vector2.down);
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger"); // Ensure this trigger exists in your Animator
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger");
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger");
                    Bork.Play();
                }
            }
        }
        else
        {
            return;
        }
    }

    void Move(Vector2 direction)
    {
       // int collisionDetect = rb.Cast(direction, movementFilter, castCollisions, moveDistance + collisionOffset);
        
        transform.position += (Vector3)(direction * moveDistance);
        
        
    }
}