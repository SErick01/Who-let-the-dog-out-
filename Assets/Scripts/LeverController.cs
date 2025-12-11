
using UnityEngine;

public class LeverController: MonoBehaviour 
{
    private bool isRight = true;
    [Min(0f)] public float speedUpVal = 25f;

    public enum LeverMode 
    { 
        Reverse, 
        SpeedUp,
        ResetSpeed,
        ObjectToggle, //Swaps object activity states. 
    }
    public LeverMode mode = LeverMode.SpeedUp;

    [Tooltip("Leave blank for Object Toggles")]
    [SerializeField] private TrainMover connectedTrain;

    [Tooltip("Optional extra trains controlled by this lever.")]
    [SerializeField] private TrainMover[] extraTrains;

    [Header("Lever Sprites")]
    public Sprite spriteUp;
    public Sprite spriteRight;
    public Sprite spriteLeft;

    private SpriteRenderer sr;
    private bool playerInside = false;
    private bool hasChosenSide = false;

    [Header("Object Toggles")]
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private GameObject[] objectsToDeactivate;

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

        if (mode == LeverMode.Reverse ||
            mode == LeverMode.SpeedUp ||
            mode == LeverMode.ResetSpeed)
        {
            ApplyModeToTrain(connectedTrain);

            if (extraTrains != null)
            {
                for (int i = 0; i < extraTrains.Length; i++)
                {
                    ApplyModeToTrain(extraTrains[i]);
                }
            }
        }
        else if (mode == LeverMode.ObjectToggle)
        {
            IsToggled = !IsToggled;

            for (int i = 0; i < objectsToActivate.Length ;i++)
            {
                objectsToActivate[i].SetActive(IsToggled);
            }

            for (int i = 0; i < objectsToDeactivate.Length ;i++)
            {
                objectsToDeactivate[i].SetActive(!IsToggled);
            }
        }
    }

    private void ApplyModeToTrain(TrainMover train)
    {
        if (train == null) return;

        switch (mode)
        {
            case LeverMode.Reverse:
                train.ReverseDirection();
                Debug.Log("Train Reversed: " + train.name);
                break;

            case LeverMode.SpeedUp:
                train.SpeedUp(speedUpVal);
                Debug.Log("Train Sped Up: " + train.name);
                break;

            case LeverMode.ResetSpeed:
                train.ResetToDefaultSpeed();
                Debug.Log("Train RESET speed: " + train.name);
                break;
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
