using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float jumpForce = 7f;
    public int playerSpeed = 16;
    public Canvas canvas;
    private bool isTouchGround = true;
    Rigidbody rb;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;

    // Rotation variables
    public float rotationSpeed = 5f;
    private float rotationAngle = 10f;
    public float lerpSpeed = 5f; // Controls how fast the rotation returns to the original rotation
    private float currentRotation = 0f;
    private float targetRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }
    
    // Update is called once per frame
    void Update()
    {
        isTouchGround = Physics.Raycast(transform.position, Vector3.down, 1, groundLayer);
        rb.velocity = new Vector3(0f, rb.velocity.y, forwardSpeed);

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
