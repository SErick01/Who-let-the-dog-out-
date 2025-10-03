using System.Collections; //cannot be Systems.Collections.Generic if want you want to use a coroutine
using UnityEngine;

public class CycleController : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    //[SerializeField] private float jumpHeight = 2f;
    //[SerializeField] private float jumpSpeed = 2f;
    //[SerializeField] private float timeBTWNJump = 2f;
   // [SerializeField] private float JumpLength = 1f;
    //[SerializeField] private float jumpDistance = 2f;
    [SerializeField] private bool movingLeft = true;
    public static bool stopAllCars = false;
    private bool isJumping;
    Rigidbody2D rigidCycle;


    private float screenBoundary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;
        stopAllCars = false;
        Rigidbody2D  rigidCycle = GetComponent<Rigidbody2D >();

       // StartCoroutine(CycleJump());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        if (movingLeft)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
        if (!stopAllCars)
        {
            //move car
            transform.Translate(direction * speed * Time.deltaTime);

            //after hit screen boundary; "respawn/ move back"
            if (movingLeft && transform.position.x < -screenBoundary)
            {
                transform.position = new Vector3(screenBoundary, transform.position.y, transform.position.z);
            }
            else if (!movingLeft && transform.position.x > screenBoundary)
            {
                transform.position = new Vector3(-screenBoundary, transform.position.y, transform.position.z);
            }
        }



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
