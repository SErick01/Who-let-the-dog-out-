using UnityEngine;

public class DirectionSwitch : MonoBehaviour
{
    [Header("True = spawn from LEFT (move right)")]
    public bool isRight = true;

    [Header("Optional lever art")]
    public Sprite leftSprite;
    public Sprite rightSprite;

    [Header("Press this while inside the trigger")]
    public KeyCode interactKey = KeyCode.E;

    bool playerInside;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ApplySprite();
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(interactKey))
        {
            isRight = !isRight;
            ApplySprite();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {if (other.CompareTag("Player")) playerInside = true;}

    void OnTriggerExit2D(Collider2D other)
    {if (other.CompareTag("Player")) playerInside = false;}

    void ApplySprite()
    {
        if (!sr) return;
        sr.sprite = isRight ? rightSprite : leftSprite;
    }
}
