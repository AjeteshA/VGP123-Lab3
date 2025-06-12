using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public float jumpForce = 5f; // Jump force of the player
    public float groundCheckRadius = 0.2f; 
    public LayerMask isGroundLayer; 
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private new Collider2D collider;
    private bool Attack = false;

    private Vector2 groundCheckPos => new Vector2(collider.bounds.min.x + collider.bounds.extents.x, collider.bounds.min.y);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider =  GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, isGroundLayer);
        bool ifGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, isGroundLayer);        
        bool isFalling = rb.linearVelocity.y < 0.01f && !ifGrounded;
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        float vDirection = rb.linearVelocity.y < 0 ? -1 : (rb.linearVelocity.y > 0 ? 1 : 0);
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y); 

        if (hInput != 0) sr.flipX = hInput < 0;

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", ifGrounded);
        anim.SetBool("isFalling", isFalling);
        anim.SetBool("Attack", Attack);
        

        if (vDirection < 0) isFalling = true;
    
    //Attack Function
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
    {
    Attack = true;
    }
    else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
    {
        Attack = false;
    }
    Debug.Log(Attack);
    }
}
