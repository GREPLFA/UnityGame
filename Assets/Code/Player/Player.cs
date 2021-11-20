using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playSpeed = 12.0f;
    public float jumpPower = 11.0f;
    Rigidbody2D rigid;
    Animator animator;

    private float move;
    private bool isJump = false;
    private bool isLife= false;
    private bool isRun = false;
    public bool gameStart = false;
    public bool gameOver = false;

    public int life = 100;
    public float timer = 0.0f;

    //Sound
    public AudioClip audioRun;
    public AudioClip audioHit;
    AudioSource audioSource;

    private GUIStyle guiStyle = new GUIStyle();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.R))
        {
            gameStart = true;
        }
        if (gameStart)
        {
            if (life > 0)
            {
                timer += Time.deltaTime;
                Move();
                Reversal();
                Jump();
                Animation();
                
            }
            else
            {
                Life();
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }

    void Life()
    {
        if (!isLife)
        {
            animator.SetBool("isDead", false);
            isLife = true;
            gameOver = true;
        }
    }


    void Move()
    {
        move = Input.GetAxis("Horizontal") * playSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + move, transform.position.y);
        
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else
            {
                return;
            }
        }
    }

    // character reversal
    void Reversal()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // character animation (true / false)
    void Animation()
    {
        // move
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
            audioSource.clip = audioRun;
            audioSource.Play();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
        
        if (!isLife)
        {
            if (collision.gameObject.tag == "Monster" && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                audioSource.Stop();
                audioSource.clip = audioHit;
                audioSource.Play();
                animator.SetTrigger("isHit");
                life -= 25;
            }
        }
    }

    private void OnGUI()
    {
        if (gameStart)
        {
            if (gameOver)
            {
                guiStyle.fontSize = 25;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 270, 100, 100), "재시작 키 'R'", guiStyle);
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 225, 100, 100), "버틴시간 : " + timer.ToString("0.0"), guiStyle);
            }
            else
            {
                guiStyle.fontSize = 50;
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 200, 100, 100), timer.ToString("0.0"), guiStyle);
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 300, 100, 100), "Life : " + life.ToString(), guiStyle);
            }
        }
        else
        {
            guiStyle.fontSize = 25;
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 100, 100, 100), "게임을 시작할려면 'R' 키를 눌러주세요.", guiStyle);
        }
    }
}