using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Variables")]
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    private float horizontal;
    private bool isFacingRight;

    private float moveDirection;


    [Header("Ground Check")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    //Touch Inputs Variables
    private Vector2 initialTouchPos;
    private bool isTouching;

    void Start (){
        rb = GetComponent<Rigidbody2D>();

        isTouching = false;
    }

    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded()){
            rb.AddForce(transform.up * jumpingPower, ForceMode2D.Impulse);

            Debug.Log("Eso Tilin");
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && rb.gravityScale != 1){
            rb.gravityScale = 1f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)){
            rb.gravityScale = -1f;
        }

        Flip();
        TouchInput();
    }

    private void FixedUpdate(){
        // rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip(){
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void TouchInput(){

        //For Knowing if there is at least one touch on Screen
        if(Input.touchCount > 0){

            //Gets the first touch
            Touch touch = Input.GetTouch(0);

            switch (touch.phase){
                case TouchPhase.Began:
                    initialTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    isTouching = true;
                    rb.gravityScale = 1f;
                break;

                case TouchPhase.Stationary:
                    if (isTouching){

                        //To Know the diff between initial pos and final pos
                        Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 delta = currentTouchPos - initialTouchPos;

                        if(delta != Vector2.zero){
                            //Give the X of the difference to a float variable
                            moveDirection = delta.normalized.x;
                        }
                        else{
                            moveDirection = 0f;
                        }
                    }
                break;
                
                case TouchPhase.Ended:
                    isTouching = false;
                    moveDirection = 0f;
                    rb.gravityScale = -1f;
                break;
            }
            //Switch End
        }
        else{
            isTouching = false;
            moveDirection = 0f;
        }
    }
}
