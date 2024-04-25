// This script represents message panel in the game.

#region Import Namespaces

using UnityEngine;
using UnityEngine.UI;

#endregion

#region Core Implementation

public class MessagePanel : MonoBehaviour
{
    private Button _startButton; // Reference to the start button in the panel.

    // Initialize references.
    private void Awake()
    {
        _startButton = GetComponentInChildren<Button>(); // Find the start button within the panel.
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Check if this is the first time showing the message panel.
        if (!PlayerPrefs.HasKey("FirstTimeMessage"))
        {
            // If it's the first time, set a PlayerPrefs key and pause the game.
            PlayerPrefs.SetInt("FirstTimeMessage", 1);
            Time.timeScale = 0;
        }
        else
        {
            // If it's not the first time, deactivate the message panel.
            gameObject.SetActive(false);
        }
    }

    // Method called when the start button is clicked.
    public void StartGame()
    {
        // Resume the game by setting the time scale to 1.
        Time.timeScale = 1;
    }
}

#endregion