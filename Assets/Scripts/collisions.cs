using System.Collections;
using UnityEngine;

public class collisions : MonoBehaviour
{
    public static bool winGame = false;
    public static bool GameOver = false;
    public static bool obstacleCheck = false;

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
        if (collision.CompareTag("Car"))
        {
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
    }

    IEnumerator DelayLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(scenedelay);
        LoadScene.Instance.LoadSceneByName(sceneName);
    }

}
