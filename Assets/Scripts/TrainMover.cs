using UnityEngine;

public class TrainMover : MonoBehaviour
{

    [Tooltip("True = train moves left, False = train moves right.")]
    [SerializeField] private bool movingLeft = false;

    [Min(0f)][SerializeField] private float speed = 25f;

    private float defaultSpeed;
    private float screenBoundary;

    void Start()
    {
        defaultSpeed = speed;
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect*2;
    }

    void Update()
    {
        
        Vector3 direction = movingLeft ? Vector3.left : Vector3.right;

        
        transform.Translate(direction * speed * Time.deltaTime);

        
        if (movingLeft && transform.position.x < -screenBoundary)
        {
            transform.position = new Vector3(screenBoundary, transform.position.y, transform.position.z);
        }
        else if (!movingLeft && transform.position.x > screenBoundary)
        {
            transform.position = new Vector3(-screenBoundary, transform.position.y, transform.position.z);
        }
    }
   
    public void ReverseDirection()
    {
        movingLeft = !movingLeft;
        Debug.Log("Train reversed! Now movingLeft = " + movingLeft);
    }

    public void SpeedUp(float amount = 2f)
    {
        speed += amount;
        Debug.Log("Train speed increased! Now speed = " + speed);
    }

    public void ResetToDefaultSpeed()
    {
        speed = defaultSpeed;
        Debug.Log("Train speed RESET! Now speed = " + speed);
    }
}
