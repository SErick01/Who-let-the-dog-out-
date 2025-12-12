using UnityEngine;

public class JumpTrigger: MonoBehaviour 
{
    [SerializeField]
    private CycleController motorcycle;
    [SerializeField]
    private Lv23Controller lv_2_3_vehicle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            if(motorcycle)
            {
                motorcycle.TriggerJump();
            }
            else if (lv_2_3_vehicle)
            {
                lv_2_3_vehicle.TriggerJump();
            }
        }
    }
}
