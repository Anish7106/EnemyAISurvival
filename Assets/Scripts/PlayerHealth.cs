using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public TMP_Text healthText;

    void Start()
    {
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        UpdateHealth();

        if (health <= 0)
        {
            GameManager manager = FindAnyObjectByType<GameManager>();

            if (manager != null)
            {
                manager.GameOver();
            }
        }
    }

    void UpdateHealth()
    {
        healthText.text = "HP: " + health;
    }
}