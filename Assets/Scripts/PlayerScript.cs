using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    private int prevent = 0;
    private int preventt = 0;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    private bool facingRight = true;
    public int test = 0;
    public int tests = 2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        musicSource.loop = true;

        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.text = "";
        lives.text = livesValue.ToString();

        musicSource.Stop();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
            test = 1;
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetWinText ();
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            SetWinText ();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            tests = 1;
            if (test == tests)
            {
                anim.SetInteger("State", 0);
                test = 0;
                tests = 2;
            }
        }

    }

    void SetWinText ()
    {
        if (scoreValue == 4 && prevent == 0)
            {
                transform.position = new Vector2(37.87f, 0.03f); 
                livesValue = 3;
                lives.text = livesValue.ToString();
                prevent += 1;

            }
            if (scoreValue >= 8 && preventt == 0)
            {
                musicSource.loop = false;
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                win.text = "You win! Game created by Andrew Strunk.";
                preventt += 1;
            }
            if (livesValue <= 0 && preventt == 0)
            {
                win.text = "You lose";
                Destroy(this);
            }
    }
    
    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }

}
