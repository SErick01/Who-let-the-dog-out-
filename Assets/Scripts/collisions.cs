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
   // private BoxCollider2D obstacle_collider;
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
       // obstacle_collider = GetComponentInParent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car") & shelterCheck == false)
        {
            Debug.Log("shelterCheck on car collision trigger: " + shelterCheck);
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
                StartCoroutine(DelayLoadScene("Done_3.3"));
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
         //   obstacle_collider.excludeLayers = new LayerMask { "Obstacles" };
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
            await Task.Delay(300);
            shelterCheck = false;
            Debug.Log("shelterCheck: " + shelterCheck);
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

