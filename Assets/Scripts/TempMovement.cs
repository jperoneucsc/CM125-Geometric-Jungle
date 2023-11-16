using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float jumpForce = 4f;
    public int playerSpeed = 6;
    public Canvas canvas;
    private bool isTouchGround = true;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }
    
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, forwardSpeed);

        Move();
        Jump();
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rb.AddForce(movement * playerSpeed);
    }

    void Jump()
    {
        //if( Input.GetKey(KeyCode.Space) && isTouchGround == true)
        if (Input.GetButtonDown("Jump") && isTouchGround == true)
        {
            Vector3 jumpForceVector = new Vector3(0f, jumpForce, 0f);
            rb.AddForce(jumpForceVector, ForceMode.Impulse);
            isTouchGround = false;
            Debug.Log("jumping");
        }
        
    }

    private void OnCollisionStay()
    {
        isTouchGround = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Respawn"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void RestartScene()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
