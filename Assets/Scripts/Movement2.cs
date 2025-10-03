using NUnit.Framework;
using System.Collections.Generic;
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
    public AudioSource bork;
    private Stack<Vector2> revertMoveRegister = new Stack<Vector2>(); //For tracking player movement
    

    void Start()
    {
        animator = GetComponent<Animator>();
        collisions.GameOver = false;
        collisions.winGame = false;
        collisions.obstacleCheck = false;
        revertMoveRegister.Clear();
    }

    // Update is called once per frame

   
    void Update()
    {
        if (!collisions.winGame && !collisions.GameOver && !collisions.obstacleCheck)
        {
            //moves up and down
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
                revertMoveRegister.Push(Vector2.down);
                Debug.Log(revertMoveRegister.Peek());
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger"); // Ensure this trigger exists in your Animator
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {

                Move(Vector2.down);
                revertMoveRegister.Push(Vector2.up);
                Debug.Log(revertMoveRegister.Peek());
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger"); // Ensure this trigger exists in your Animator
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
                revertMoveRegister.Push(Vector2.right);
                Debug.Log(revertMoveRegister.Peek());
                if (animator != null)
                {
                    animator.SetTrigger("MoveTrigger");
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
                revertMoveRegister.Push(Vector2.left);
                Debug.Log(revertMoveRegister.Peek());
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
                    bork.Play();
                }
            }
        }
        else if (collisions.obstacleCheck)
        {
            Move(revertMoveRegister.Pop()); //Reverts the last movement command for obstacle clearance
            Debug.Log("Reverted to:");
            Debug.Log(revertMoveRegister.Peek());
            collisions.obstacleCheck = false;
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