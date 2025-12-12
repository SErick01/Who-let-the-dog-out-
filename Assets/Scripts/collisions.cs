using System.Collections;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class collisions : MonoBehaviour
{
    public static bool winGame = false;
    public static bool GameOver = false;
    public static bool obstacleCheck = false;
    public static bool shelterCheck = false;

    private Animator animator;
    private BoxCollider2D obstacle_collider;
    [SerializeField] private LayerMask obstaclesMask;
    //[SerializeField] public GameObject Parent;
    [SerializeField] public GameObject triggerColliderParent;
    [SerializeField] public GameObject mainColliderParent;
    public AudioSource soundtrack;
    public AudioSource gameOver;
    public AudioSource dog_CRASH;
    public AudioSource victory;

    public LeverController lever;


    private float scenedelay = 4.0f; //delays next scene by n number of seconds
    public string sceneName => SceneManager.GetActiveScene().name;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        //obstacle_collider = Parent.GetComponentInParent<BoxCollider2D>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car") && shelterCheck == false || collision.CompareTag("Train") && shelterCheck == false)
        {
            Debug.Log("shelterCheck on car/train collision trigger: " + shelterCheck);
            animator.SetTrigger("CrashTrigger");
            GameOver = true;
            soundtrack.Stop();
            gameOver.Play();
            dog_CRASH.Play();

            // add delay to Loading Scene
            if (sceneName == "Level_1" || sceneName == "Level_1.2" || sceneName == "Level_1.3")
            {
                StartCoroutine(DelayLoadScene("GameOver_1"));
            }
            else if (sceneName == "Level_2" || sceneName == "Level_2.2" || sceneName == "Level_2.3")
            {
                StartCoroutine(DelayLoadScene("GameOver_2"));
            }
            else 
            {
                StartCoroutine(DelayLoadScene("GameOver_3"));
            }

        }
        else if (collision.CompareTag("Cat"))
        {
            winGame = true;
            soundtrack.Stop();
            animator.SetTrigger("VictoryTrigger");
            victory.Play();

            //add delay to Loading Scene; not immediate
            if (sceneName == "Level_1")
            {
                StartCoroutine(DelayLoadScene("Complete_1.1"));
            }
            else if (sceneName == "Level_1.2")
            {
                StartCoroutine(DelayLoadScene("Complete_1.2"));
            }
            else if (sceneName == "Level_1.3")
            {
                StartCoroutine(DelayLoadScene("Complete_1.3"));
            }
            else if (sceneName == "Level_2")
            {
                StartCoroutine(DelayLoadScene("Finish_2.1"));
            }
            else if (sceneName == "Level_2.2")
            {
                StartCoroutine(DelayLoadScene("Finish_2.2"));
            }
            else if (sceneName == "Level_2.3")
            {
                StartCoroutine(DelayLoadScene("Finish_2.3"));
            }
            else if (sceneName == "Level_3")
            {
                StartCoroutine(DelayLoadScene("Done_3.1"));
            }
            else if (sceneName == "Level_3.2")
            {
                StartCoroutine(DelayLoadScene("Done_3.2"));
            }
            else if (sceneName == "Level_3.3")
            {
                StartCoroutine(DelayLoadScene("Outro1"));
            }

        }

        else if (collision.CompareTag("Static_Obstacle"))
        {
            obstacleCheck = true;
        }
        else if (collision.CompareTag("Ramp"))
        {
            obstacleCheck = true;
        }
        else if (collision.CompareTag("Shelter"))
        {
            shelterCheck = true;
            triggerColliderParent.layer = LayerMask.NameToLayer("No_Collision");
            mainColliderParent.layer = LayerMask.NameToLayer("No_Collision");
            //Debug.Log("Peanut collider excluded collision layers: " + obstacle_collider.excludeLayers.value);
            //obstacle_collider.excludeLayers = obstaclesMask;
            //Debug.Log("Peanut collider excluded collision layers: " + obstacle_collider.excludeLayers.value);
            Debug.Log("Obstacle mask applied?");
            Debug.Log("shelterCheck: " + shelterCheck);
        }
        else if (collision.CompareTag("Lever"))
        {
            Debug.Log("Lever collision detected!");

            if (lever != null)
            {
                
                lever.ToggleLever();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Shelter"))
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            //Parent.GetComponentInParent<BoxCollider2D>().enabled = false;
            if (obstacleCheck != false)
            {
                obstacleCheck = false;
            }
            if (shelterCheck != true)
            {
            shelterCheck = true;
            Debug.Log("shelterCheck: " + shelterCheck);
            }
        }
    }

    private async void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shelter")) {
            await Task.Delay(1000);
            shelterCheck = false;
            Debug.Log("shelterCheck: " + shelterCheck);
            triggerColliderParent.layer = LayerMask.NameToLayer("Animals");
            mainColliderParent.layer = LayerMask.NameToLayer("Animals");
            //GetComponent<BoxCollider2D>().enabled = true;
            //Parent.GetComponentInParent<BoxCollider2D>().enabled = true;
            //Debug.Log("Peanut collider excluded collision layers: " + obstacle_collider.excludeLayers.value);
            //obstacle_collider.excludeLayers = new LayerMask { };
            //Debug.Log("Peanut collider excluded collision layers: " + obstacle_collider.excludeLayers.value);
        }
    }

    IEnumerator DelayLoadScene(string sceneName)
    {
        Debug.Log("Trying to load " +  sceneName);
        yield return new WaitForSeconds(scenedelay);
        if(LoadScene.Instance)
            LoadScene.Instance.LoadSceneByName(sceneName);
        else
        {
            Debug.LogError("You don't have a Load Scene component!");
        }
    }

}
