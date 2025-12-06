
using UnityEngine;

public class LeverController: MonoBehaviour 
{
    private bool isRight = true;
    [Min(0f)] public float speedUpVal = 25f;

    public enum LeverMode 
    { 
        Reverse, 
        SpeedUp,
        ObjectToggle, //Swaps object activity states. 
    }
    public LeverMode mode = LeverMode.SpeedUp;

    [Tooltip("Leave blank for Object Toggles")]
    [SerializeField] private TrainMover connectedTrain;

    [Header("Lever Sprites")]
    public Sprite spriteUp;
    public Sprite spriteRight;
    public Sprite spriteLeft;

    private SpriteRenderer sr;
    private bool playerInside = false;
    private bool hasChosenSide = false;

    [Header("Object Toggles")]
    [SerializeField]
    private GameObject[] objectsToActivate;
    [SerializeField]
    private GameObject[] objectsToDeactivate;

    [SerializeField]
    private bool IsToggled; //starts false 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("LeverController: No SpriteRenderer found on this GameObject.");
        }
    }

    void Start()
    {
        UpdateLeverVisual();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            ToggleLever();
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInside = false;
        }
    }

    public void ToggleLever()
    {
        //For lever Visuals
        if (!hasChosenSide)
        {
            hasChosenSide = true;
            isRight = true;
        } else {
            isRight = !isRight;
        }
        UpdateLeverVisual();

        if (connectedTrain != null)
        {
            if (mode == LeverMode.Reverse)
            {
                connectedTrain.ReverseDirection();
                Debug.Log("Train Reversed");
            }
            else if (mode == LeverMode.SpeedUp)
            {
                connectedTrain.SpeedUp(speedUpVal);
                Debug.Log("Train Sped Up");
            }
        }
        else
        {
            //Turn on and off given objects. 
            IsToggled = !IsToggled;
            if (mode == LeverMode.ObjectToggle)
            {
                for (int i = 0; i < objectsToActivate.Length; i++)
                {
                    objectsToActivate[i].SetActive(IsToggled);
                }
                for (int i = 0; i < objectsToDeactivate.Length; i++)
                {
                    objectsToDeactivate[i].SetActive(!IsToggled);
                }
            }

            //TODO connect to Light post for feedback -- show bright light sprites for any tracks that have active trains. Otherwise lights off. 
        }
    }

    private void UpdateLeverVisual()
    {
        if (sr == null) return;

        if (!hasChosenSide)
        {
            if (spriteUp != null)
            {
                sr.sprite = spriteUp;
            } else {
                sr.sprite = spriteRight;
            }
        } else {
            sr.sprite = isRight ? spriteRight : spriteLeft;
        }
    }
}
