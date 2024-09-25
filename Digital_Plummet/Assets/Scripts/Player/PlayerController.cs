using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement Variables")]
    
    [Tooltip("Speed multiplier for velocity gain. How fast can the player go.")]
    [Min(1f)] [SerializeField] private float moveSpeed;

    [Tooltip("It limits general speed in X axis.")]
    [SerializeField] private float maxSpeedX;

    [Tooltip("It limits general speed in Y axis.")]
    [SerializeField] private float maxSpeedY;

    [Tooltip("Is literally to apply power to jump with 'Spacebar' (The character doesn't jump). I don't know why it's here.")]
    [SerializeField] private float jumpingPower;
    
    [Tooltip("Limits the X maximums values for the velocity in change direction. It affects mostly horizontal control.")]
    [SerializeField] private float maxChangeDirVel;

    [Tooltip("Multiplier for air Control, Bigger = Easier to move.")]
    [Min(0.01f)] [SerializeField] private float airMultiplier;

    [Tooltip("Multiplies gravity force for extra down acceleration")]
    [Min (0f)] [SerializeField] private float gravMultiplier;

    [Tooltip("The counter vector for gravity change, helps to stop.")]
    [Range(0.1f, 1f)] [SerializeField] private float counterGravForce;

    [Tooltip("Deceleration velocity to stop in the X axis.")]
    [Range(0.01f, 1f)][SerializeField] private float decelerationVel;

    private float horizontal;
    private bool isFacingRight;

    private float moveDirection;
    private float moveMouseDirection;
    private Vector2 flatVel;
    private Vector2 lastVel;

    private Vector2 gravityChangeForce;


    [Header("Ground Check")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;


    //Life Variables
    [Header("Life Variables")]
    [SerializeField] private bool inmortal;
    private int life;
    

    //Touch Inputs Variables
    [Header("Touch Inputs Variables")]

    [Tooltip("Tolerance number for the minimun 'delta' required between touch points to move the character")]
    [Range (10000f, 35000f)] [SerializeField] private float touchMinTolerance;

    private Vector2 initialTouchPos;
    private bool isTouching;


    //Mouse Inputs Variables
    private TextMeshProUGUI mouseStatus;
    private GameObject mouseStatusObject;
    private bool activateMouse;
    private Vector2 initialMousePos;
    private bool isHoldingClick;


    //Effect of traps on player variables
    private bool onEMPEffect;

    //Respawn Variables
    [Header("MENUS")]
    [Tooltip("Menu for the moment the player faces DEATH D:")]
    [SerializeField] GameObject deadMenu;
    private UIManager uiManager;
    [SerializeField] GameObject playerStart;

    //Tutorial Variables
    private bool tutorialBlock;

    //Tutorial Variables
    [Header("VFX")]
    [Tooltip("Falling Down Particle System")]
    [SerializeField] ParticleSystem fallingDownVFX;

    [Tooltip("Falling Down Particle System")]
    [SerializeField] ParticleSystem fallingUpVFX;


    public bool TutorialBlock{
        get{ return tutorialBlock; }
        set{ tutorialBlock = value; }
    }

    private void Awake()
    {
        deadMenu = GameObject.Find("PanelMuerte");
        uiManager = deadMenu.GetComponent<UIManager>();
        playerStart = GameObject.Find("PlayerStart");
        Debug.Log(deadMenu.name);
    }

    void Start (){
        rb = GetComponent<Rigidbody2D>();

        mouseStatusObject = GameObject.Find("Mouse Status");
        //mouseStatus = mouseStatusObject.GetComponent<TextMeshProUGUI>();

        life = 1;

        isTouching = false;

        activateMouse = false;

        tutorialBlock = false;

        onEMPEffect=false;

        fallingDownVFX.Stop();
        fallingUpVFX.Play();
    }

    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(transform.up * jumpingPower, ForceMode2D.Impulse);
        }

        Flip();

        if(Input.GetKeyDown(KeyCode.P)){
            activateMouse = !activateMouse;
        }

        if (!onEMPEffect){
            if (activateMouse)
            {
                MouseInput();
                //mouseStatus.text = "Mouse ON";
                //mouseStatus.color = Color.green;
            }
            else
            {
                TouchInput();
                //mouseStatus.text = "Mouse OFF";
                //mouseStatus.color = Color.red;
            }
        }

        Debug.Log(flatVel);
        SpeedControl();
    }

    private void FixedUpdate(){

        if (activateMouse){
            rb.AddForce(new Vector2(moveMouseDirection, 0f) * moveSpeed * Time.deltaTime * airMultiplier, ForceMode2D.Force);
            Debug.Log("Velocity: " + rb.velocity);

            GravityMultiplier();
        }
        else{
            rb.AddForce(new Vector2(moveDirection, 0f) * moveSpeed * Time.deltaTime * airMultiplier, ForceMode2D.Force);

            GravityMultiplier();
        }
    }

    private void SpeedControl(){
        flatVel = new Vector2(rb.velocity.x, rb.velocity.y);

        if (MathF.Abs(flatVel.x) > maxSpeedX){
            Vector2 limitedVel = flatVel.normalized * maxSpeedX;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }
        else if (MathF.Abs(flatVel.y) > maxSpeedY){
            Vector2 limitedVel = flatVel.normalized * maxSpeedY;
            rb.velocity = new Vector2(rb.velocity.x, limitedVel.y);
        }
    }

    private void GravityMultiplier(){
        rb.AddForce(Vector2.down * gravMultiplier, ForceMode2D.Force);
    }

    private IEnumerator Deceleration(){
        while(Mathf.Abs(lastVel.x) > 0.1f){

            if(lastVel.x < 0f){
                rb.velocity = new Vector2(lastVel.x + decelerationVel, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(lastVel.x - decelerationVel, rb.velocity.y);
            }

            lastVel = rb.velocity;

            yield return null;
        }
    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip(){
        if(isFacingRight && moveDirection < 0f || !isFacingRight && moveDirection > 0f){
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
            gravityChangeForce.y = rb.velocity.y * counterGravForce;
            rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
        }

        if(Input.GetMouseButton(0)){
            if (tutorialBlock == false){
                Vector2 currentTouchPos = Camera.main.ViewportToScreenPoint(Input.mousePosition);
                float delta = currentTouchPos.x - initialMousePos.x;
                if(Mathf.Abs(delta) >= touchMinTolerance){
                    moveMouseDirection = Mathf.Clamp(delta, -maxChangeDirVel, maxChangeDirVel);

                    Debug.DrawLine(initialMousePos, new Vector3(delta, transform.position.y, 0f), Color.red);
                }
            }
            else{
                moveMouseDirection = 0f;
            }
            
            
            lastVel = rb.velocity;
        }

        if(Input.GetMouseButtonUp(0)){
            StartCoroutine(nameof(Deceleration));

            moveMouseDirection = 0f;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            isHoldingClick = false;
            rb.gravityScale = -1f;

            gravityChangeForce.x = rb.velocity.x;
            gravityChangeForce.y = rb.velocity.y * counterGravForce;
            rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
        }
    }

    private void TouchInput(){

        //For Knowing if there is at least one touch on Screen
        if (Input.touchCount > 0){

            //Gets the first touch
            Touch touch = Input.GetTouch(0);

            if(!fallingDownVFX.isPlaying || fallingUpVFX.isPlaying){
                fallingDownVFX.Play();
                fallingUpVFX.Stop();
            }

            switch (touch.phase){
                case TouchPhase.Began:
                    initialTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                    isTouching = true;
                    rb.gravityScale = 1f;

                    gravityChangeForce.x = rb.velocity.x;
                    gravityChangeForce.y = rb.velocity.y * counterGravForce;
                    rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);

                break;

                case TouchPhase.Moved:
                    if(!tutorialBlock){
                        //To Know the diff between initial pos and final pos
                        Vector2 currentTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                        float delta = currentTouchPos.x - initialTouchPos.x;
                        if (Mathf.Abs(delta) >= touchMinTolerance)
                        {
                            moveDirection = Mathf.Clamp(delta, -maxChangeDirVel, maxChangeDirVel);

                            Debug.DrawLine(initialTouchPos, currentTouchPos, Color.red);
                        }
                    }
                    else{
                        moveDirection = 0f;
                    }
                    
                    lastVel = rb.velocity;
                    break;
                
                case TouchPhase.Ended:
                    StartCoroutine(nameof(Deceleration));

                    moveDirection = 0f;
                    rb.velocity = new Vector2(0f, rb.velocity.y);

                    isTouching = false;
                    rb.gravityScale = -1f;

                    gravityChangeForce.x = rb.velocity.x;
                    gravityChangeForce.y = rb.velocity.y * counterGravForce;
                    rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);
                break;
            }
            //Switch End
        }
        else if (Input.touchCount <= 0){
            if(fallingDownVFX.isPlaying){
                fallingDownVFX.Stop();
                fallingUpVFX.Play();
            }   
        }
    }

    public void TakeDmg(){
        if(!inmortal){
            life -= 1;
            playerStart.SetActive(false);
            
            if(uiManager != null)
            { 
                uiManager.PanelFadeIn();
            }
            else
            {
                Debug.Log("No se encontro UI MANAGER");
            }

            Debug.Log("Busted");

            OnDeathCharacter();
        }
        else{
            //nothing...
        }
        
    }

    public void EMPHit(float duration){
        StartCoroutine(nameof(EMPActivate), duration);
    }

    private IEnumerator EMPActivate(float duration){
        onEMPEffect=true;
        yield return new WaitForSeconds(duration);
        onEMPEffect=false;
    }

    public void ResetCharacter(){
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        rb.isKinematic = false;
    }

    private void OnDeathCharacter(){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
}


