using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float flapStrength=5.0f;
    public float gravity = -9.8f;
    private Vector3 velocity;
    public Sprite[] sprites;
    private int spriteIndex=0;
    public bool isAlive = false;
    public AudioSource audioSource;
    public AudioClip play;
    public AudioClip pause;
    public GameObject startGame;
    public bool isRunning = true;
    public GameObject resumeButton;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        startGame.SetActive(false);
        //Debug.Log(gameObject.name);
        //animation calling
        //InvokeRepeating("Animation",0.15f,0.15f);
        InvokeRepeating(nameof(Animation), 0.15f, 0.15f);
    }

    private void OnEnable()
    {   Vector3 newPosition = transform.position;
        newPosition.y = 0;
        transform.position = newPosition;
        velocity = Vector3.zero;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = Vector3.up * flapStrength;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                velocity = Vector3.up * flapStrength;
            }
        }
        velocity.y = velocity.y + gravity * Time.deltaTime;
        transform.position += velocity*Time.deltaTime;
        if (transform.position.y >= 5)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isRunning)
            {
                StopGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    private void Animation()
    {
        spriteIndex=(spriteIndex+1)% sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (collision.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
        
    }
    void StopGame()
    {
        isRunning=false;
        audioSource.Stop();

        // Freeze time
        Time.timeScale = 0f;

        // Show pause menu or overlay
        resumeButton.SetActive(true);
    }

    public void ResumeGame()
    {
        isRunning = true;
        audioSource.Play();
        Time.timeScale = 1f;
        resumeButton.SetActive(false);
    }



}
