using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TempMovement : MonoBehaviour
{
    [SerializeField] EndlessLevelGameManager gameManager;

    public PlayableDirector playableDirector;

    public float forwardSpeed = 15f;
    public float jumpForce = 20f;
    public int playerSpeed = 150;
    public float playerMass = 3.0f;
    public float playerFallingMass = 4.0f;
    public Canvas canvas;
    public bool isTouchGround = true;
    public Rigidbody rb;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;

    // Player health variables
    public int maxHealth = 3;
    public int currentHealth;
    public bool isInvulnerable = false;
    private float invulnerabilityTime = 2.0f;

    // Additional components
    private Animator animator;
    private CapsuleCollider hurtbox;
    PlayerDamageFlash damageFlash;
    private bool cutsceneFinished = false;

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
        hurtbox = GetComponent<CapsuleCollider>();
        damageFlash = GetComponent<PlayerDamageFlash>();

        currentHealth = maxHealth;

        rb.isKinematic = true;

        playableDirector.stopped += OnTimelineFinished;
        // Play the timeline
        playableDirector.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(cutsceneFinished)
        {
            // The update function still runs when timescale is 0, so this line fixes weird pausing issues
            if (Time.timeScale == 0)return;

            isTouchGround = Physics.Raycast(transform.position, Vector3.down, 0.5f, groundLayer);
            animator.SetBool("isGrounded", isTouchGround);
            rb.velocity = new Vector3(0f, rb.velocity.y, forwardSpeed);

            // If the player is falling downwards, increase weight to make a weightier trajectory
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

            // If the player is damaged, flash
            if (isInvulnerable)
            {
                //Debug.Log("Ow");
            }
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

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Obstacles"))
        {
            // Get collider's audio script
            ObstacleSFX collideSFX = collisionObject.GetComponent<ObstacleSFX>();
            collideSFX.PlaySound();

            // Hide renderer and collider
            MeshRenderer collisionRenderer = collisionObject.GetComponent<MeshRenderer>();
            MeshCollider collisionCollider = collisionObject.GetComponent<MeshCollider>();
            BoxCollider collisionBoxCollider = collisionObject.GetComponent<BoxCollider>();

            if (collisionRenderer == null || (collisionCollider == null && collisionBoxCollider == null))
            {
                Destroy(collisionObject);
            }
            else
            {
                if (collisionCollider != null)
                {
                    collisionRenderer.enabled = false;
                    collisionCollider.enabled = false;
                }
                else if (collisionBoxCollider != null)
                {
                    collisionRenderer.enabled = false;
                    collisionBoxCollider.enabled = false;
                }
            }

            if (!isInvulnerable)
            {
                Damage();
            }
        }
    }

    // Health management functions
    public void Damage()
    {
        currentHealth -= 1;

        if (currentHealth <= 0)
        {
            Die();
        }
        else 
        {
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        damageFlash.EnableFlash();
        yield return new WaitForSeconds(invulnerabilityTime);
        damageFlash.DisableFlash();
        isInvulnerable = false;
    }

    public void Die()
    {
        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.PlayerDied();
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        // Unregister the callback
        director.stopped -= OnTimelineFinished;

        // Set the flag indicating the cutscene has finished
        cutsceneFinished = true;
        rb.isKinematic = false;
    }
}
