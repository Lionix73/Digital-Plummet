using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerV2 : MonoBehaviour
{

    [Header("Movement Variables")]
    
    [Tooltip("Speed multiplier for velocity gain. How fast can the player go.")]
    [SerializeField] private float moveSpeed;

    [Tooltip("It limits general speed in X axis.")]
    [SerializeField] private float maxSpeedX;

    [Tooltip("It limits general speed in Y axis.")]
    [SerializeField] private float maxSpeedY;

    [Tooltip("Multiplies gravity force for extra down acceleration")]
    [Min (0f)] [SerializeField] private float gravMultiplier;

    [Tooltip("The counter vector for gravity change, helps to stop.")]
    [Range(0.1f, 1f)] [SerializeField] private float counterGravForce;

    private float horizontal;
    private bool isFacingRight;

    private float moveDirection;
    private float moveMouseDirection;
    private Vector2 flatVel;
    private Vector2 lastVel;

    private Vector2 gravityChangeForce;

    private Rigidbody2D rb;


    //Life Variables
    [Header("Life Variables")]
    [SerializeField] private bool inmortal;
    private int life;
    

    //Touch Inputs Variables
    [Header("Touch Inputs Variables")]

    [Tooltip("Tolerance number for the minimun 'delta' required between touch points to move the character")]
    [Range (0.1f, 5f)] [SerializeField] private float touchMinTolerance;

    private Vector2 initialTouchPos;
    private Vector2 touchTemp;
    private float delta;
    private bool isTouching;


    //Effect of traps on player variables
    private bool onEMPEffect;


    //Respawn Variables
    [Header("MENUS")]
    [Tooltip("Menu for the moment the player faces DEATH D:")]
    [SerializeField] private GameObject deadMenu;
    [SerializeField] private GameObject playerStart;
    private CinemachineFollowCharacter cineMachineFollowCharacter;
    private UIManager uiManager;


    //Tutorial Variables
    private bool tutorialBlock;


    //Tutorial Variables
    [Header("VFX")]
    [Tooltip("Falling Down Particle System")]
    [SerializeField] private ParticleSystem fallingDownVFX;

    [Tooltip("Falling Down Particle System")]
    [SerializeField] private ParticleSystem fallingUpVFX;

    //Animation
    private Animator animator;
    private bool spawning;


    public bool TutorialBlock{
        get{ return tutorialBlock; }
        set{ tutorialBlock = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        deadMenu = GameObject.Find("PanelMuerte");
        uiManager = deadMenu.GetComponent<UIManager>();
        playerStart = GameObject.Find("PlayerStart");
        cineMachineFollowCharacter = FindObjectOfType<CinemachineFollowCharacter>();
        Debug.Log(deadMenu.name);
    }

    void Start (){
        //spawning=true;

        rb = GetComponent<Rigidbody2D>();

        life = 1;

        isTouching = false;

        tutorialBlock = false;

        onEMPEffect=false;

        fallingDownVFX.Stop();
        fallingUpVFX.Play();
    }

    void Update(){

        Flip();

        if (!onEMPEffect){
            TouchInput();
        }
        
        Debug.DrawLine(initialTouchPos, touchTemp, Color.red);

        SpeedControl();
    }

    private void FixedUpdate(){
        // Obtener la posición actual del jugador
            Vector2 currentVelocity = rb.velocity;

        if (isTouching)
        {
            Debug.Log(moveDirection);

            if(Mathf.Abs(delta) > touchMinTolerance){
                rb.velocity = new Vector2(moveDirection * moveSpeed, currentVelocity.y);
            }
        }

        GravityMultiplier();
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

    private void Flip(){
        if(isFacingRight && moveDirection < transform.position.x || !isFacingRight && moveDirection > transform.position.x){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void TouchInput(){
        Vector2 currentTouchPos;

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
                    initialTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    isTouching = true;
                    rb.gravityScale = 1f;

                break;

                case TouchPhase.Moved:
                    if(!tutorialBlock){
                        // Actual Pos Depending on the world
                        currentTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchTemp = currentTouchPos;
                        delta = currentTouchPos.x - initialTouchPos.x;

                        moveDirection = Mathf.Clamp(delta, -4f, 4f);
                    }
                    
                break;

                case TouchPhase.Ended:
                    isTouching = false;
                    rb.gravityScale = -1f;

                    initialTouchPos = Vector2.zero;
                    currentTouchPos = Vector2.zero;
                    moveDirection = 0f;
                    
                    gravityChangeForce.x = rb.velocity.x;
                    gravityChangeForce.y = rb.velocity.y * counterGravForce;
                    rb.AddForce(-gravityChangeForce, ForceMode2D.Impulse);

                break;
                case TouchPhase.Canceled:
                    isTouching = false;
                    rb.gravityScale = -1f;

                    initialTouchPos = Vector2.zero;
                    currentTouchPos = Vector2.zero;
                    moveDirection = 0f;
                    
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

        cineMachineFollowCharacter.FindAndFollow();
    }

    private void OnDeathCharacter(){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        isTouching = false;
    }

    public void SpawnAnim(){
        StartCoroutine(nameof(CutMovement),2.0f);
    }

    private IEnumerator CutMovement(float timer){
        yield return new WaitForSeconds(timer);
        spawning=false;
        animator.SetBool("spawning", spawning);
    }
}

