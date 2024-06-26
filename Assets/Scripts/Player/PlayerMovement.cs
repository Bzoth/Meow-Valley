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
    public Camera mainCamera;
    public Vector3 mousePosition;
    public Transform player;
    public GameObject interactPoint;
    public GameObject rotationPoint, up, down, left, right;

    [SerializeField] private string lookDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Transform>();
    }
    
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        Vector3 direction = new Vector3(horizontal, vertical);

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 diffrence= new Vector2 (player.position.x - mousePosition.x, player.position.y - mousePosition.y);

        if(mousePosition.x < player.position.x && Mathf.Abs(diffrence.x) > Mathf.Abs(diffrence.y))
        {
            interactPoint.transform.position = left.transform.position;
            lookDirection = "Left";
        }
        if(mousePosition.x > player.position.x && Mathf.Abs(diffrence.x) > Mathf.Abs(diffrence.y))
        {
            interactPoint.transform.position = right.transform.position;
            lookDirection = "Right";
        }
        if(mousePosition.y < player.position.y && Mathf.Abs(diffrence.x) < Mathf.Abs(diffrence.y))
        {
            interactPoint.transform.position = down.transform.position;
            lookDirection = "Down";
        }
        if(mousePosition.y > player.position.y && Mathf.Abs(diffrence.x) < Mathf.Abs(diffrence.y))
        {
            interactPoint.transform.position = up.transform.position;
            lookDirection = "Up";
        }

        //Delete after find a use
        if(lookDirection == "Test")
        {
            print("Test");
        }

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
