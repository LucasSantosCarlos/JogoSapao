using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D colliderPlayer;
    private Animator anim;
    private float moveX;
    private bool jumpPressed;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    public int life;
    public TextMeshProUGUI textLife;
    public string levelName;
    public GameObject gameOver;
    public float alturaLimite;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }

        textLife.text = life.ToString();

        if(life <= 0)
        { 
            gameOver.SetActive(true);
        }

        if (transform.position.y < alturaLimite)
        {
            gameOver.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Move();

        if (isGrounded)
        {
            addJumps = 1;
            if(jumpPressed)
            {
                Jump();
            }
        }
        else
        {
            if (jumpPressed && addJumps > 0){
                addJumps--;
                Jump();
            }
        }

        jumpPressed = false;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX > 0) 
        {
            spriteRenderer.flipX = false;
            anim.SetBool("isRun", true);
        }

        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("isRun", true);
        }

        if (moveX == 0) 
        {
            anim.SetBool("isRun", false);
        }

    }

    void Jump()
    {
         rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
         anim.SetBool("isJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            life--;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
