using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBoxTimer : MonoBehaviour
{
    [SerializeField]
    private Image boxTimer; // Reference to the UI Image to display the timer

    public float timerDuration = 5f; // Timer duration in seconds
    private float timer; // Current timer value
    private bool isTimerRunning = false; // Flag to track if the timer is active

        public GameObject FloatingTextPrefab;
        public GameObject celebration;



    public void Start()
    {
        if (boxTimer != null)
        {
            boxTimer.gameObject.SetActive(false); // Hide the timer UI initially
            boxTimer.fillAmount = 1f; // Ensure the timer UI is full initially
        }
        timer = timerDuration; // Initialize the timer with the duration
    }

    public void StartTimer()
    {
        if (boxTimer != null)
        {
            boxTimer.gameObject.SetActive(true); // Show the timer UI
        }
        timer = timerDuration; // Reset the timer
        isTimerRunning = true; // Start the timer
    }

    void Update()
    {
        if (isTimerRunning && timer > 0)
        {
            timer -= Time.deltaTime; // Decrease the timer value
            if (boxTimer != null)
            {
                boxTimer.fillAmount = timer / timerDuration; // Update the UI
            }

            if (timer <= 0)
            {
                isTimerRunning = false; // Stop the timer when it reaches zero
                showFloatingText();
                showCelebration();
                gameObject.SetActive(false);
                Debug.Log("Timer Finished!");
            }
        }
    }

 void showFloatingText()
{
    if (FloatingTextPrefab != null)
    {
        // Instantiate the floating text as its own GameObject (not a child)
        GameObject floatingText = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);

        // Optionally, adjust the position (this will be absolute in world space)
        floatingText.transform.position = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);

        // Set the Y rotation to 180 degrees
        floatingText.transform.rotation = Quaternion.Euler(0, 180, 0);

        // Set the text to "Box Added!"
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = "Box Added";

            // Set the text color to CF6A13
            Color textColor;
            if (ColorUtility.TryParseHtmlString("#CF6A13", out textColor))
            {
                textMesh.color = textColor; // Apply the color
            }
            else
            {
                Debug.LogWarning("Failed to parse color.");
            }
        }
    }
}


void showCelebration()
{
    if (celebration != null)
    {
        // Instantiate the floating text as its own GameObject (not a child)
        GameObject floatingText = Instantiate(celebration, transform.position, Quaternion.identity);
    }
}



}
