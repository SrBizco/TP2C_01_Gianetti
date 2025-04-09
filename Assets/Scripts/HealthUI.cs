using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private DroneHealth playerHealth;
    [SerializeField] private Image fillImage;

    void Start()
    {
        if (playerHealth != null)
        {
            healthSlider.maxValue = playerHealth.MaxHealth;
            healthSlider.value = playerHealth.CurrentHealth;
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            float current = playerHealth.CurrentHealth;
            healthSlider.value = current;

            UpdateHealthColor(current / playerHealth.MaxHealth);
        }
    }

    void UpdateHealthColor(float healthPercent)
    {
        if (healthPercent > 0.66f)
        {
            fillImage.color = Color.green;
        }
        else if (healthPercent > 0.33f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }
}