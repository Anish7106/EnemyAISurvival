using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public EnemyHealth enemyHealth;

    void Start()
    {
        slider.maxValue = enemyHealth.health;
        slider.value = enemyHealth.health;
    }

    void Update()
    {
        slider.value = enemyHealth.health;
    }
}