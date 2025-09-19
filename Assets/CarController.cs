using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool movingLeft = true;

    private float screenBoundary;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;
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

        //move car
        transform.Translate(direction * speed * Time.deltaTime);


        //after hit screen boundary; "respawn/ move back"
        if (movingLeft && transform.position.x < -screenBoundary) {
            transform.position = new Vector3(screenBoundary, transform.position.y, transform.position.z);
        }
        else if (!movingLeft && transform.position.x > screenBoundary)
        {
            transform.position = new Vector3(-screenBoundary, transform.position.y, transform.position.z);
        }
    }
}
