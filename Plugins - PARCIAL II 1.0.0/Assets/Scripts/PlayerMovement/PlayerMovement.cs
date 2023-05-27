using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int health;
    public TMP_Text chestsText;
    public TMP_Text healthText;

    private int ChestLeft;
    [Header("Movement - Moving")]
    
    private float playerMoveSpeed;
    [SerializeField] private float playerWalkSpeed;
    [SerializeField] private float playerSprintSpeed;
    

    [Header("Movement - Jumping")]
    
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;
    
    [Header("Keybinds")]
    
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    
    public float playerHeight;
    public LayerMask whatIsGround;
    public float groundDrag;
    private bool isGrounded;
    
    //VARIABLES PARA QUE FUNCIONE EL CODIGO
    public Transform orientation;
    //Variables para guardar el input de las teclas
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public MovementState currentState;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    };
    void Start()
    {
        ChestLeft = 10;
        chestsText.text = $"Chests Left: {ChestLeft}";
        healthText.text = $"Health: {health}";
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        if (health <= 0 || ChestLeft <= 0)
        {
            Debug.Log("GAME FINISHED");
            SceneManager.LoadScene("GameOver");
        }
        
        
        //Debug.Log(rb.velocity.magnitude);
        //Checamos si esta en el piso con un raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.3f, whatIsGround);
        //Debug.DrawRay(transform.position,Vector3.down * (playerHeight + 0.3f),Color.green);
        
        KeyboardInput();
        SpeedControl();
        StateHandler();
        
        //Debug.Log("Is grounded = " + isGrounded + " Ground Drag: " + rb.drag + " Ready to jump:" + readyToJump + " RB Velocity: " + rb.velocity.magnitude);
        
        
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        healthText.text = $"Health: {health}";
        MovePlayer();
    }

    private void KeyboardInput()
    {
        //Get Input for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        //Jump test
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJumpCoolDown),jumpCooldown);
        }
    }

    private void StateHandler()
    {
        //Seting State
        if (isGrounded && Input.GetKey(sprintKey))
        {
            currentState = MovementState.sprinting;
            playerMoveSpeed = playerSprintSpeed;
        }
        else if (isGrounded)
        {
            currentState = MovementState.walking;
            playerMoveSpeed = playerWalkSpeed;
        }
        else
        {
            currentState = MovementState.air;
        }
    }
    private void MovePlayer()
    {
        //Calcular la direccion del movimiento
        //Dos vectores con escalares sumados
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //on ground TODO ESTE IF ->
        if (isGrounded)
        {
            rb.AddForce(moveDirection * playerMoveSpeed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection * playerMoveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        //In air
        // else if (!isGrounded)
        // {
        //     
        // }
    }

    private void SpeedControl()
    {
        Vector3 playerRawVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //Si la velocidad que estamos consiguiendo "raw" es superior a PlayerMoveSpeed 
        if (playerRawVelocity.magnitude > playerMoveSpeed)
        {
            //Se normaliza flatVelocity (es decir magnitud 1 pero mantiene la direccion) y lo multiplicamos por el escalar playerMoveSpeed
            Vector3 limitedVelocity = playerRawVelocity.normalized * playerMoveSpeed;
            //Se le asigna la velocidad correcta al rigidbody
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    //JUMPING 
    private void Jump()
    {
        //Reseteamos la velocidad en Y para que no se Stackeen los saltos y siempre saltemos la misma altura
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    } 
    private void ResetJumpCoolDown()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemies>() != null)
        {
            health -= 25;
        }
        if (other.gameObject.GetComponent<Minions>() != null)
        {
            health -= 25;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chest")
        {
            Debug.Log("COLISION!");
            Destroy(other.gameObject);
            ChestLeft--;
            chestsText.text = $"Chests Left: {ChestLeft}";
        }

        if (other.tag == "MusicBoss")
        {
            
        }
    }
}
