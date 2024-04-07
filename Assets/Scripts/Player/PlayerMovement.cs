using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed = 5;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        Vector3 direction = new Vector3(horizontal, vertical);

        AnimateMovement(direction);

        if(rb.velocity != Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", rb.velocity.x);
            anim.SetFloat("LastVertical", rb.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    void AnimateMovement(Vector3 direction)
    {
        if(anim != null)
        {
            if(direction.magnitude > 0)
            {
                anim.SetBool("IsMoving", true);

                anim.SetFloat("Horizontal", direction.x);
                anim.SetFloat("Vertical", direction.y);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }
    }
}
