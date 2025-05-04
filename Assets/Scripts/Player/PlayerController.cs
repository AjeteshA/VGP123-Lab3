using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public float jumpForce = 5f; // Jump force of the player
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     float hInput = Input.GetAxisRaw("Horizontal");
     float vInput = Input.GetAxisRaw("Vertical");
    if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

     rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y); 
    }
}
