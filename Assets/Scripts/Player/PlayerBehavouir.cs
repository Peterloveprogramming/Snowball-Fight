using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject normalAvatar; // The first GameObject to deactivate
    [SerializeField]
    private GameObject stupidAvatar; // The second GameObject to activate


    [SerializeField]
    private float _maxHealth = 3;
    [SerializeField]
    private GameObject _deathEffect, _hitEffect;

    private float currentHealth;
    [SerializeField]
    private HealthBar _healthbar;

    private MoveObject move;

    //text 
    public GameObject FloatingTextPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // get move 
        move = GetComponent<MoveObject>();

        // Initialize health
        currentHealth = _maxHealth;

        if (_healthbar != null)
            _healthbar.UpdateHealthBar(_maxHealth, currentHealth);

        // Initialize eyes
        if (normalAvatar != null) normalAvatar.SetActive(true);
        if (stupidAvatar != null) stupidAvatar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Add player input or movement logic here if needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PLAYER hit");

        move.updateHit();
        // Deactivate normal eyes and activate hit effects
        if (normalAvatar != null) normalAvatar.SetActive(false);
        if (stupidAvatar != null) stupidAvatar.SetActive(true);

        // Decrease health
        currentHealth -= Random.Range(0.5f, 1.5f);

        // Update health bar
        if (_healthbar != null)
            _healthbar.UpdateHealthBar(_maxHealth, currentHealth);

        //show floating text
        if (FloatingTextPrefab != null){
            showFloatingText();
        }
        // Check for death
        if (currentHealth <= 0)
        {
 if (_deathEffect != null)
{
    Debug.Log("PLAYER died");

    // Get the world space position and rotation of the current GameObject
    Vector3 worldPosition = transform.position;
    Quaternion worldRotation = transform.rotation;

    // Create an empty GameObject at the same position and rotation
    GameObject emptyParent = new GameObject("DeathEffectParent");
    emptyParent.transform.position = worldPosition;
    emptyParent.transform.rotation = worldRotation;

    // Instantiate the death effect as a child of the empty GameObject
    GameObject deathEffectInstance = Instantiate(_deathEffect, worldPosition, Quaternion.identity, emptyParent.transform);

    Debug.Log("Death effect instantiated as a child of the new GameObject at: " + worldPosition);

    // Destroy the current GameObject
    Destroy(gameObject);
}

        }
        else
        {
            // Trigger hit effect if not dead
            if (_hitEffect != null)
            {
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            }
        }
    }

    void showFloatingText()
    {
        if (FloatingTextPrefab != null)
        {
            GameObject floatingText = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            
            // Optionally, adjust the position relative to the parent
            floatingText.transform.localPosition = new Vector3(0, 1.15f, 0.2f);

            // Set the Y rotation to 180 degrees
            floatingText.transform.localRotation = Quaternion.Euler(0, 180, 0);
            floatingText.GetComponent<TextMesh>().text = "Stunned"; 
        }
    }
}
