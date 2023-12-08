using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMenu : MonoBehaviour
{
    public Canvas title;
    public Canvas credits;

    // Start is called before the first frame update
    public void goToCredits()
    {
        // Hide the current canvas
        if (title != null)
        {
            title.gameObject.SetActive(false);
        }

        // Show the new canvas
        if (credits != null)
        {
            credits.gameObject.SetActive(true);
        }
    }
    
    public void goToMain()
    {
        // Hide the current canvas
        if (credits != null)
        {
            credits.gameObject.SetActive(false);
        }

        // Show the new canvas
        if (title != null)
        {
            title.gameObject.SetActive(true);
        }
    }
}
