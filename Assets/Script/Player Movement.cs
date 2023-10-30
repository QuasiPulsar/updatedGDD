using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;

    public float walkSpeed = 9f; // Normal walk speed
    public float crouchWalkSpeed = 3f; // Speed while crouch walking

    private bool isGrounded = false;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        float speed = Input.GetKey(KeyCode.S) ? crouchWalkSpeed : walkSpeed;
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 9f);
        }
       
      
        UpdateAnimationUpdate();
    }
    private void UpdateAnimationUpdate()
    {
        if (dirX > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("jumping", true);
        }
        else
        {
            anim.SetBool("jumping", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("crouching", true);

            if (dirX != 0f)
            {
                anim.SetBool("crouchwalk", true);
            }
            else
            {
                anim.SetBool("crouchwalk", false);
            }
        }
        else
        {
            anim.SetBool("crouching", false);
            anim.SetBool("crouchwalk", false);
        }

        

    }

    private bool IsGrounded()
    {
        Collider2D collider = GetComponent<Collider2D>();
        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        return hit.collider != null;
    }


}
