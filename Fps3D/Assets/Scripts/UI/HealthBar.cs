using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        if (health <= 0) health = 100;
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        if (health < 0) health = 0;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
