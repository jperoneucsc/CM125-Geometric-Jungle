using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float jumpForce = 20f;
    public int playerSpeed = 150;
    public float playerMass = 3.0f;
    public float playerFallingMass = 4.0f;
    public Canvas canvas;
    private bool isTouchGround = true;
    Rigidbody rb;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;

    // Additional components
    private Animator animator;

    // Rotation variables
    public float rotationSpeed = 10f;
    private float rotationAngle = 10f;
    public float lerpSpeed = 5f; // Controls how fast the rotation returns to the original rotation
    private float currentRotation = 0f;
    private float targetRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // The update function still runs when timescale is 0, so this line fixes weird pausing issues
        if (Time.timeScale == 0)return;

        isTouchGround = Physics.Raycast(transform.position, Vector3.down, 0.5f, groundLayer);
        animator.SetBool("isGrounded", isTouchGround);
        rb.velocity = new Vector3(0f, rb.velocity.y, forwardSpeed);

        // If the player is falling downwards, increase weight to make a nicer trajectory
        if (rb.velocity.y < 0 && !isTouchGround)
        {
            rb.mass = playerFallingMass;
        } else {
            rb.mass = playerMass;
        }
        

        Move();
        if (Input.GetButtonDown("Jump") && isTouchGround == true)
        {
            Jump();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rb.AddForce(movement * playerSpeed);

        // Rotate the player slightly based on horizontal input
        if (Mathf.Abs(horizontalInput) > 0.3f)
        {
            float rotationAmount = Mathf.Clamp(horizontalInput * rotationAngle, -rotationAngle, rotationAngle);
            currentRotation += rotationAmount * Time.deltaTime * rotationSpeed;
            currentRotation = Mathf.Clamp(currentRotation, -rotationAngle, rotationAngle);
            transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
        }else
        {
            // Smoothly interpolate between the current and target rotation
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
    }

    void Jump()
    {
        Vector3 jumpForceVector = new Vector3(0f, jumpForce, 0f);
        rb.AddForce(jumpForceVector, ForceMode.Impulse);
        isTouchGround = false;
    }


    public void RestartScene()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
