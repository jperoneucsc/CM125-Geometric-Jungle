using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelGameManager : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pause menu inputs
        if (Input.GetButtonDown("Pause")) 
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
    }
}
