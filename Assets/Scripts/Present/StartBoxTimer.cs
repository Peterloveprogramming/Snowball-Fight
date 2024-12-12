using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBoxTimer : MonoBehaviour
{
    [SerializeField]
    private Image boxTimer; // Reference to the UI Image to display the timer

    [SerializeField]
    private string presentReceivedText; // Reference to the UI Image to display the timer

    [SerializeField]
    private Camera _cam; // Assign the VR camera manually or dynamically

    public float timerDuration = 5f; // Timer duration in seconds
    private float timer; // Current timer value
    private bool isTimerRunning = false; // Flag to track if the timer is active

    public GameObject FloatingTextPrefab;
    public GameObject celebration;

    // get snow and teleport to lock them up while touching present
    public SpawnSnow spawnSnow;
    public UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.ActionBasedControllerManager teleportController;


    public void resetTimer(){
        isTimerRunning = false;
        timer = 0f;
        boxTimer.gameObject.SetActive(false);
    }

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
        faceCamera();
        timer = timerDuration; // Reset the timer
        isTimerRunning = true; // Start the timer
        disableTelportAndSnowSpawn();
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
                enableTelportAndSnowSpawn();
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


             if (_cam != null)
            {
                 Vector3 direction = _cam.transform.position - transform.position;
                direction.y = 0; // Ignore Y-axis rotation for horizontal alignment

                // Rotate to face the VR camera only on the Y-axis

                floatingText.transform.rotation = Quaternion.LookRotation(direction);
                floatingText.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
    

        // Set the text to "Box Added!"
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = presentReceivedText;

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
             if (_cam != null)
            {
                 Vector3 direction = _cam.transform.position - transform.position;
                direction.y = 0; // Ignore Y-axis rotation for horizontal alignment

                // Rotate to face the VR camera only on the Y-axis

                floatingText.transform.rotation = Quaternion.LookRotation(direction);
            }
    }
}

public void disableTelportAndSnowSpawn()
{
    spawnSnow.setLockSpawn(true);
    teleportController.setLockTeleport(true);

}

public void enableTelportAndSnowSpawn()
{
    spawnSnow.setLockSpawn(false);
    teleportController.setLockTeleport(false);

}


    private void faceCamera(){
            if (_cam != null)
            {
                // Calculate the direction to the VR camera
                Vector3 direction = _cam.transform.position - transform.position;
                direction.y = 0; // Ignore Y-axis rotation for horizontal alignment

                // Rotate to face the VR camera only on the Y-axis
                transform.rotation = Quaternion.LookRotation(direction);
            }
    }

}
