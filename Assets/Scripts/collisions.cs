using System.Collections;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class collisions : MonoBehaviour
{
    public static bool winGame = false;
    public static bool GameOver = false;
    public static bool obstacleCheck = false;
    public static bool shelterCheck = false;

    private Animator animator;
    public AudioSource soundtrack;
    public AudioSource gameOver;
    public AudioSource dog_CRASH;
    public AudioSource victory;


    public float scenedelay = 9.0f; //delays next scene by n number of seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
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
            StartCoroutine(DelayLoadScene("BEnd"));
        }
        else if (collision.CompareTag("Cat"))
        {
            winGame = true;
            soundtrack.Stop();
            animator.SetTrigger("VictoryTrigger");
            victory.Play();

            //add delay to Loading Scene; not immediate
            StartCoroutine(DelayLoadScene("GEnd1"));
            
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
            Debug.Log("shelterCheck: " + shelterCheck);
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
            await Task.Delay(2000);
            shelterCheck = false;
            Debug.Log("shelterCheck: " + shelterCheck);
        }
    }

    IEnumerator DelayLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(scenedelay);
        LoadScene.Instance.LoadSceneByName(sceneName);
    }

}
