using UnityEditor.VersionControl;
using UnityEngine;

public class LeverController: MonoBehaviour 
{

        [Header("Lever State")]
        public bool isRight = true;
        private bool playerInside = false;

    public enum LeverMode { Reverse, SpeedUp }
        [SerializeField] private LeverMode mode = LeverMode.Reverse; 
        [SerializeField] private TrainMover connectedTrain; //assign train

        [Header("Animation")]
        public Transform leverHandle; 
        public float angleRight = 30f; 
        public float angleLeft = -30f; 
        public float animationSpeed = 5f;

        private Quaternion targetRotation;

        void Start()
        {
            UpdateLeverVisual();
        }

        void Update()
        {
            if (leverHandle)
            {
                leverHandle.localRotation = Quaternion.Lerp(
                    leverHandle.localRotation,
                    targetRotation,
                    Time.deltaTime * animationSpeed
                );
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")&& !playerInside)
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
            isRight = !isRight;
            UpdateLeverVisual();

            if (connectedTrain != null)
            {
                if (mode == LeverMode.Reverse)
                {
                    connectedTrain.ReverseDirection();
                    Debug.Log($"Train Reversed");

            }
                else if (mode == LeverMode.SpeedUp)
                {
                    connectedTrain.SpeedUp(6f);
                    Debug.Log($"Train Sped Up");
            }
        }
    }

        private void UpdateLeverVisual()
        {
            float angle = isRight ? angleRight : angleLeft;
            targetRotation = Quaternion.Euler(angle, 0f, 0f);
        }
 }

