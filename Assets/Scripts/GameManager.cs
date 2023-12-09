using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndlessLevelGameManager : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] DeathScreen deathScreen;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;

    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;

    bool canPause = true;
    bool canScore = false;

    int score = 0;
    float scoreUpdateTime = 0.2f;
    float scoreTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pause menu inputs
        if (Input.GetButtonDown("Pause") && canPause) 
        {
            if (pauseMenu.isActive)
            {
                pauseMenu.Resume();
            }
            else 
            {
                pauseMenu.Pause();
            }
        }

        if (canScore)
        {
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= scoreUpdateTime)
            {
                score += 1;
                scoreText.text = "Score: " + score.ToString("D6");

                // Reset the timer
                scoreTimer = 0f;
            }
        }
    }

    public void BeginPlay()
    {
        canScore = true;
    }

    public void CollectableCollected()
    {
        score += 50;
    }

    public void PlayerHit(int newHealth)
    {
        if (newHealth >= 3)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
        }
        if (newHealth == 2)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartEmpty;
        }
        if (newHealth == 1)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
        }
        if (newHealth <= 0)
        {
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
        }
    }

    public void PlayerDied()
    {
        canPause = false;
        deathScreen.ShowScreen();
    }
}
