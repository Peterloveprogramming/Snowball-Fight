using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionReference_Instantiate; // Input action reference for instantiation

    [SerializeField]
    private GameObject prefabToInstantiate; // Prefab to instantiate

    [SerializeField]
    private Transform spawnPoint; // Optional spawn point


    public bool snowOnCooldown = false;
    public float snowOnCooldownTime = 2.5f;
    public float currentTime = 0f; // Changed from bool to float


    void Update()
    {
        if (snowOnCooldown)
        {
            // Increment currentTime by the time passed since the last frame
            currentTime += Time.deltaTime;

            // Check if the cooldown period has passed
            if (currentTime >= snowOnCooldownTime)
            {
                currentTime = 0f; // Reset current time
                snowOnCooldown = false; // End cooldown
            }
        }
    }

    private void OnEnable()
    {
        // Subscribe to input action
        inputActionReference_Instantiate.action.performed += InstantiatePrefab;
    }

    private void OnDisable()
    {
        // Unsubscribe from input action
        inputActionReference_Instantiate.action.performed -= InstantiatePrefab;
    }

    /// <summary>
    /// Instantiates the prefab at the specified spawn point or the origin if no spawn point is set.
    /// Logs "triggered" to the console when the action is performed.
    /// </summary>
    /// <param name="context">Input action callback context</param>
    private void InstantiatePrefab(InputAction.CallbackContext context)
    {
        Debug.Log("triggered");

        if (prefabToInstantiate != null && !snowOnCooldown )
        {
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            Quaternion spawnRotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

            // Instantiate the prefab
            Instantiate(prefabToInstantiate, spawnPosition, spawnRotation);
            snowOnCooldown = true;
        }
        else
        {
            Debug.LogWarning("Prefab to instantiate is not assigned!");
        }
    }
}
