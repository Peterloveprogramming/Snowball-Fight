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

        if (prefabToInstantiate != null)
        {
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            Quaternion spawnRotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

            // Instantiate the prefab
            Instantiate(prefabToInstantiate, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogWarning("Prefab to instantiate is not assigned!");
        }
    }
}
