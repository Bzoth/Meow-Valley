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
        anim.SetFloat("HorizontalRun", Mathf.Abs(horizontal));
        
        Vector3 scaler = transform.localScale;
        if (scaler.x == 1 && rb.velocity.x < 0)
        { scaler.x *= -1; }
        else if (scaler.x == -1 && rb.velocity.x > 0)
        { scaler.x *= -1; }
        transform.localScale = scaler;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
}
