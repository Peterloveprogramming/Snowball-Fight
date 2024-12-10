using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _healthbarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (_healthbarSprite != null)
        {
            // Update the fill amount of the health bar
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}
