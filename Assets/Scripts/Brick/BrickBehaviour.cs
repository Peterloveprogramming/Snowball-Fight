using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    private bool triggerCoroutine = false;
    private bool hasBeenHitBySnow = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snow"))
        {
            Debug.Log("Hit by snow");
            hasBeenHitBySnow = true;
        }

        if (collision.gameObject.CompareTag("Teleport"))
        {
            if (hasBeenHitBySnow)
            {
                Debug.Log("Teleport triggered");
                triggerCoroutine = true;
            }
        }
    }

    void Update()
    {
        // Check the condition
        if (triggerCoroutine)
        {
            // Reset the condition to avoid repeatedly starting the coroutine
            triggerCoroutine = false;

            // Start the coroutine
            StartCoroutine(DestroyBrick());
        }
    }

    IEnumerator DestroyBrick()
    {
        // Wait for 5 seconds before destroying the object
        yield return new WaitForSeconds(5);

        Debug.Log("Brick is being destroyed after delay");

        // Destroy the brick
        Destroy(this.gameObject);
    }
}
