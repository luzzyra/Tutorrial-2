using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public TextMeshProUGUI livesText; 
    private Rigidbody2D rd2d;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float speed;

    public Text score;
    private int lives;
    Animator anima;
    bool the = true;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;
    bool music = true;
    private int scoreValue = 0;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        anima = GetComponent<Animator>();

        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives = 3;
        SetLivesText();
        if (music == true)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = true;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if(hozMovement > 0)
        {
            gameObject.transform.localScale = new Vector3(3, 3, 2);
        }
        if (hozMovement < 0)
        {
            gameObject.transform.localScale = new Vector3(-3, 3, 2);
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        if (isOnGround == false)
        {
            anima.SetInteger("State", 3);
        }
        if (isOnGround == true)
        {
            anima.SetInteger("State", 0);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anima.SetInteger("State", 1);

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anima.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anima.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anima.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anima.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anima.SetInteger("State", 0);
        }
        




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {

            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue >= 4 && the == true)
        {
            transform.position = new Vector4(100.0f, 0.0f, 0.0f);
            lives = 3;
            SetLivesText();
            the = false;
        }
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipThree;
            musicSource.Play();
           
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anima.SetInteger("State", 3);
            }
            if (isOnGround == false)
            {
                anima.SetInteger("State", 3);
            }
            if (isOnGround == true)
            {
                anima.SetInteger("State", 0);
            }
        }
        if (isOnGround == false)
        {
            anima.SetInteger("State", 3);
        }
        if (isOnGround == true)
        {
            anima.SetInteger("State", 0);
        }

    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }

    }
}