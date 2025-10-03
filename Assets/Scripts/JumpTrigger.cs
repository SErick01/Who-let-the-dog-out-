using UnityEngine;

public class JumpTrigger: MonoBehaviour 
{
    [SerializeField]
    private CycleController motorcycle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            motorcycle.TriggerJump();
        }
    }
}
