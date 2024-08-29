using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Variables")]
    
    [Tooltip("Speed multiplier for velocity gain. How fast can the player go.")]
    [SerializeField] private float speed;

    [Tooltip("Is literally to jump with 'Spacebar'. I don't know why it's here")]
    [SerializeField] private float jumpingPower;
    
    [Tooltip("Limits the X maximums values for the velocity in change direction. It affects mostly horizontal control.")]
    [SerializeField] private float maxChangeDirVel;
    
    private float horizontal;
    private bool isFacingRight;

    private float moveDirection;
    private float moveMouseDirection;

    private Vector2 gravityChangeForce;


    [Header("Ground Check")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    //Life Variables
    private int life;


    //Touch Inputs Variables
    private Vector2 initialTouchPos;
    private bool isTouching;


    //Mouse Inputs Variables
    [Tooltip("The Text to know if the mouse is an active Input.")]
    [SerializeField] TextMeshProUGUI mouseStatus;
    private bool activateMouse;
    private Vector2 initialMousePos;
    private bool isHoldingClick;

    void Start (){
        rb = GetComponent<Rigidbody2D>();

        isTouching = false;

        activateMouse = false;

        life = 1;
    }

    void Update(){
        // horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(transform.up * jumpingPower, ForceMode2D.Impulse);
        }

        Flip();

        if(Input.GetKeyDown(KeyCode.P)){
            activateMouse = !activateMouse;
        }

        if (activateMouse)
        {
            MouseInput();
            mouseStatus.text = "Mouse ON";
            mouseStatus.color = Color.green;
        }
        else
        {
            TouchInput();
            mouseStatus.text = "Mouse OFF";
            mouseStatus.color = Color.red;
        }
    }

    private void FixedUpdate(){
        if (activateMouse){
            rb.velocity = new Vector2(moveMouseDirection * speed, rb.velocity.y);
            Debug.Log("Velocity: " + rb.velocity);
        }
        else{
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        }
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

    private void MouseInput(){
        if(Input.GetMouseButtonDown(0)){
            initialMousePos = Camera.main.ViewportToScreenPoint(Input.mousePosition);
            isHoldingClick = true;
            rb.gravityScale = 1f;

            gravityChangeForce.x = rb.velocity.x;
            gravityChangeForce.y = rb.velocity.y * 0.7f;
            rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
        }

        if(Input.GetMouseButton(0)){
            Vector2 currentTouchPos = Camera.main.ViewportToScreenPoint(Input.mousePosition);
            float delta = currentTouchPos.x - initialMousePos.x;
            if(delta != 0){
                moveMouseDirection = Mathf.Clamp(delta, -maxChangeDirVel, maxChangeDirVel);

                Debug.DrawLine(initialMousePos, new Vector3(delta, transform.position.y, 0f), Color.red);
            }
        }

        if(Input.GetMouseButtonUp(0)){
            moveMouseDirection = 0f;
            isHoldingClick = false;
            rb.gravityScale = -1f;

            gravityChangeForce.x = rb.velocity.x;
            gravityChangeForce.y = rb.velocity.y * 0.7f;
            rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
        }
    }

    private void TouchInput(){

        //For Knowing if there is at least one touch on Screen
        if (Input.touchCount > 0){

            //Gets the first touch
            Touch touch = Input.GetTouch(0);

            switch (touch.phase){
                case TouchPhase.Began:
                    initialTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                    isTouching = true;
                    rb.gravityScale = 1f;

                    gravityChangeForce.x = rb.velocity.x;
                    gravityChangeForce.y = rb.velocity.y * 0.7f;
                    rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);

                break;

                case TouchPhase.Moved:
                    //To Know the diff between initial pos and final pos
                    Vector2 currentTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                    float delta = currentTouchPos.x - initialTouchPos.x;
                    if (delta != 0)
                    {
                        moveDirection = Mathf.Clamp(delta, -maxChangeDirVel, maxChangeDirVel);

                        Debug.DrawLine(initialTouchPos, currentTouchPos, Color.red);
                    }
                    break;
                
                case TouchPhase.Ended:
                    isTouching = false;
                    moveDirection = 0f;
                    rb.gravityScale = -1f;

                    gravityChangeForce.x = rb.velocity.x;
                    gravityChangeForce.y = rb.velocity.y * 0.7f;
                    rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
                break;
            }
            //Switch End
        }
        else{
            isTouching = false;
            moveDirection = 0f;
        }
    }

    public void TakeDmg(){
        life -= 1;

        Debug.Log("Busted");
        Destroy(gameObject);
    }
}
