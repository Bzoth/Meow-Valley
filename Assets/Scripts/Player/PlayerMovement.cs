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
    public GameObject up, down, left, right;

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

        Vector3 diffrenceX = new Vector3 (player.position.x , mousePosition.x);
        Vector3 diffrenceY = new Vector3 (player.position.y , mousePosition.y);

        if(mousePosition.x < player.localPosition.x && diffrenceX.magnitude > diffrenceY.magnitude)
        {
            interactPoint.transform.position = left.transform.position;
            lookDirection = "Left";
        }
        if(mousePosition.x > player.localPosition.x && diffrenceX.magnitude > diffrenceY.magnitude)
        {
            interactPoint.transform.position = right.transform.position;
            lookDirection = "Right";
        }
        if(mousePosition.x < player.localPosition.x && diffrenceX.magnitude < diffrenceY.magnitude)
        {
            interactPoint.transform.position = down.transform.position;
            lookDirection = "Down";
        }
        if(mousePosition.x > player.localPosition.x && diffrenceX.magnitude < diffrenceY.magnitude)
        {
            interactPoint.transform.position = up.transform.position;
            lookDirection = "Up";
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
