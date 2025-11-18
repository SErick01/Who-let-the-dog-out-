using UnityEngine;

public class TrainMover : MonoBehaviour
{
    [Tooltip("True = move to screen right. False = move to screen left.")]
    public bool moveRight = true;

    [Min(0f)] public float speed = 6f;
    [Min(0f)] public float lifeSeconds = 20f;

    void Start()
    {if (lifeSeconds > 0f) Destroy(gameObject, lifeSeconds);}

    void Update()
    {
        float dir = moveRight ? 1f : -1f;
        transform.Translate(Vector3.right * dir * speed * Time.deltaTime, Space.World);
    }
}
