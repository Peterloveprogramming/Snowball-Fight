using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGhost : MonoBehaviour
{
    [SerializeField]
    private GameObject _deathEffect; // Reference to the death effect prefab
    [SerializeField]
    private GameObject _hitEffect;   // Reference to the hit effect prefab

    private GameObject avatarParent; // Reference to the child GameObject named "AvatarParent"

    void Start()
    {
        // Find the child GameObject named "AvatarParent"
        avatarParent = transform.Find("AvatarParent")?.gameObject;

        if (avatarParent == null)
        {
            Debug.LogWarning("AvatarParent not found. Please ensure the child GameObject is named correctly.");
        }
    }

    public void PlayDeathAnimation()
    {
        Debug.Log("PLAYER died");

        // Instantiate the death effect as a child of the current GameObject
        if (_deathEffect != null)
        {
            GameObject deathEffectInstance = Instantiate(_deathEffect, transform.position, Quaternion.identity, transform);
            Debug.Log("Death effect instantiated at: " + transform.position);

            // Optionally, detach the death effect from the parent
            deathEffectInstance.transform.SetParent(null);
        }
        else
        {
            Debug.LogWarning("_deathEffect prefab is not assigned.");
        }

        // Destroy the AvatarParent GameObject
        if (avatarParent != null)
        {
            Destroy(avatarParent);
            Debug.Log("AvatarParent destroyed.");
        }
        else
        {
            Debug.LogWarning("AvatarParent is already null or was not found.");
        }
    }
}
