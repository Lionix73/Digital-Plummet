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
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    private float horizontal;
    private bool isFacingRight;

    private float moveDirection;
    private float moveMouseDirection;


    [Header("Ground Check")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    //Life Variables
    private int life;


    //Touch Inputs Variables
    private Vector2 initialTouchPos;
    private bool isTouching;
    private Vector2 changeDir;


    //Mouse Inputs Variables
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

            Debug.Log("Eso Tilin");
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
            initialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isHoldingClick = true;
            rb.gravityScale = 1f;
        }

        if(Input.GetMouseButton(0)){
            Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = currentTouchPos - initialTouchPos;
            if(delta != Vector2.zero){
                Vector2 clampedMagnitude = Vector2.ClampMagnitude(delta, 2);
                moveMouseDirection = clampedMagnitude.x;

                Debug.Log(delta.x);
            }
        }

        if(Input.GetMouseButtonUp(0)){
            moveMouseDirection = 0f;
            isHoldingClick = false;
            rb.gravityScale = -1f;
        }
    }

    private void TouchInput(){

        //For Knowing if there is at least one touch on Screen
        if (Input.touchCount > 0){

            //Gets the first touch
            Touch touch = Input.GetTouch(0);

            switch (touch.phase){
                case TouchPhase.Began:
                    initialTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    isTouching = true;
                    rb.gravityScale = 1f;

                break;

                case TouchPhase.Moved:
                    //To Know the diff between initial pos and final pos
                    Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 delta = currentTouchPos - initialTouchPos;

                    if (delta != Vector2.zero)
                    {
                        //Give the X of the difference to a float variable
                        Vector2 clampedMagnitude = Vector2.ClampMagnitude(delta, 2);
                        moveDirection = clampedMagnitude.x;

                        changeDir = new Vector2(moveDirection, 0f);
                        Debug.Log(changeDir.x);
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

    public void TakeDmg(){
        life -= 1;

        Debug.Log("Busted");
        Destroy(gameObject);
    }
}
