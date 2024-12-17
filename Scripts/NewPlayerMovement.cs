using Cinemachine.Utility;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public Transform cam;
    public GameObject cams;



    


    [Header("Player stats")]
    public int MaxHealth = 100;
    public int MaxStamina = 100;
    private float currentHealth;
    private float currentStamina;
    public PlayerHealth healthBar;
    public PlayerStamina staminaBar;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float playerSpeed;

    [Header("Jump")]
    public float jumpForce = 50;
    public bool DoubleJump = false;
    private bool jumpEnable = true;

    private Rigidbody rb;

    [Header("Ground")]
    private bool isGrounded;
    public float playerHeight;

    private float smoothTime = 0.1f;
    float turnVelocity;


    [Header("Incline")]
    public float maxIncline;
    private RaycastHit inclineHit;

    [Header("Controls")]
    public KeyCode Sprint = (KeyCode.LeftShift);
    public KeyCode AltSprint = (KeyCode.RightShift);
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Diary = KeyCode.Tab;


    [Header("Pause")]
    public GameObject PauseUI;
    public bool pauseActive;

    [Header("Diary")]
    public GameObject DiaryUI;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(MaxHealth);
        currentHealth = MaxHealth;
        currentStamina = MaxStamina;
        staminaBar.SetMaxStamina(MaxStamina);
        pauseActive = PauseUI.activeSelf;
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f);

        if (isGrounded && direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, smoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;


            if (Input.GetKey(Jump) && currentStamina > 10 && jumpEnable)
            {
                moveDir += (Vector3.up * jumpForce * 0.1f);
                playerJump();
            }

            if (currentStamina > 1 && (Input.GetKey(AltSprint) || Input.GetKey(Sprint)))
            {
                //Debug.Log("Speed");
                playerSpeed = runSpeed;
                
                currentStamina -= 1;
            }
            else
            {
                playerSpeed = walkSpeed;
                currentStamina -= 0.1f;
                
            }

            

            

            

            rb.velocity = (moveDir.normalized * playerSpeed);

            if (OnIncline())
            {
                rb.velocity =((Vector3.ProjectOnPlane(moveDir, inclineHit.normal).normalized) * playerSpeed);
            }
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            //rb.AddForce(Vector3.down * 10);

        }
        jumpEnable = true;
        
        if (DialogueManager.GetInstance().dialoguePlaying)
        {
            rb.velocity = Vector3.zero;
            
            cams.SetActive(false);
            jumpEnable = false;
        }
        

        healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);
        regenStamina();


        

        
    }


    private bool OnIncline()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out inclineHit, (playerHeight * 0.5f) + 0.3f))
        {
            //Debug.Log("OnSlope");
            float inclineAngle = Vector3.Angle(Vector3.up, inclineHit.normal);
            //Debug.Log(inclineAngle);
            return inclineAngle < maxIncline && inclineAngle != 0;
        }
        //Debug.Log("NotSlope");
        return false;
    }

    void Update()
    {
        if (Input.GetKeyDown(Jump) && (isGrounded || DoubleJump) && jumpEnable)
        {
            Debug.Log("Jump");
            playerJump();
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }

        if (Input.GetKeyDown(Diary))
        {
            OnDiary();
        }
    }


    private void regenStamina()
    {
        currentStamina += 0.2f;
        if (Input.GetKeyDown(Sprint) || Input.GetKeyDown(Jump))
        {
            currentStamina -= 0.2f;
        }
        else
        {
            currentStamina += 0.2f;
        }

    }

    private void playerJump()
    {
        //rb.AddRelativeForce(Vector3.up * jumpForce * 10, ForceMode.Force);
        rb.velocity += Vector3.up * jumpForce * 2;
        currentStamina -= 10f;
    }

    void OnDiary()
    {
        DiaryUI.SetActive(!DiaryUI.gameObject.activeSelf);
        cams.SetActive(!cams.activeSelf);
    }
    public void OnPause()
    {
        pauseActive = PauseUI.activeSelf;
        Debug.Log(PauseUI.activeSelf);
        PauseUI.SetActive(!pauseActive);
        cams.SetActive(pauseActive);
        Time.timeScale = 1 - Time.timeScale;
        
        
    }




}
