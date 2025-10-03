using System.Collections; //cannot be Systems.Collections.Generic if want you want to use a coroutine
using UnityEngine;

public class CycleController : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    //[SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpSpeed = 2f;
    //[SerializeField] private float timeBTWNJump = 2f;
    // [SerializeField] private float JumpLength = 1f;
    //[SerializeField] private float jumpDistance = 2f;
    [SerializeField] private bool movingLeft = true;
    public static bool stopAllCars = false;
    private bool isJumping;
    Rigidbody2D rigidCycle;
    [SerializeField] private float velocityMagMax = 50f;
    Vector3 direction;

    private float screenBoundary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;
        stopAllCars = false;
        rigidCycle = GetComponent<Rigidbody2D>();

        // StartCoroutine(CycleJump());
        GetDirection();
    }

    void GetDirection()
    {
        if (movingLeft)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
    }

    /// <summary>
    /// Process physics - Runs based on Physics engine. Same for every player. 
    /// </summary>
    private void FixedUpdate()
    {
        //Add directional force to motorcycle
        rigidCycle.AddForce(direction * speed * Time.fixedDeltaTime);

        //we can learn about the velocity/magnitude of the rigidbody
        Debug.Log("Motorcycle velocity magnitude = " + rigidCycle.linearVelocity.magnitude);
        //Smooth clamp to velocity. 
        if(rigidCycle.linearVelocity.magnitude >  velocityMagMax)
        {
            rigidCycle.linearVelocity = Vector2.MoveTowards(rigidCycle.linearVelocity, Vector2.zero, 5* Time.fixedDeltaTime);   
        }
    }

    /// <summary>
    /// Runs every frame - dependent on the Player's machine
    /// </summary>
    void Update()
    {
        if (!stopAllCars)
        {
            //move car
            //transform.Translate(direction * speed * Time.deltaTime);

            CheckBounds();
        }

    }


    /// <summary>
    /// This can be called by hitting any kind of trigger - jump ramps for example. 
    /// </summary>
    public void TriggerJump()
    {
        rigidCycle.AddRelativeForceY(jumpSpeed, ForceMode2D.Force); 
    }

    void CheckBounds()
    {
        //after hit screen boundary; "respawn/ move back"
        if (movingLeft && transform.position.x < -screenBoundary)
        {
            transform.position = new Vector3(screenBoundary, transform.position.y, transform.position.z);
            ResetVelocity();
        }
        else if (!movingLeft && transform.position.x > screenBoundary)
        {
            transform.position = new Vector3(-screenBoundary, transform.position.y, transform.position.z);
            ResetVelocity();
        }
    }

    void ResetVelocity()
    {
        rigidCycle.linearVelocity = Vector2.zero;
    }

  // private IEnumerator CycleJump()
   // {
       // while (!stopAllCars)//keep doing until collision or all cars/cycles are stopped
       // {
           // yield return new WaitForSeconds(timeBTWNJump);
            
           // if (!stopAllCars)
           // {


           // }
           // else
            //{
             //   continue;
           // }
       // }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisions.GameOver = true;
            stopAllCars = true;
        }
    }
}
