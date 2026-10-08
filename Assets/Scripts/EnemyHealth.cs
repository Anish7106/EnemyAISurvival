using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;
    public int scoreValue = 10;

    public float knockbackForce = 0.8f;
    public float knockbackDuration = 0.15f;

    public GameObject deathEffect;
    public Transform healthBarFill;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        maxHealth = health;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        UpdateHealthBar();
    }

    public void TakeDamage(Vector3 hitPosition)
    {
        health--;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke(nameof(ResetColor), 0.15f);
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction =
                (transform.position - hitPosition).normalized;

            rb.AddForce(
                direction * knockbackForce,
                ForceMode2D.Impulse
            );
        }

        EnemyAI enemyAI = GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.Knockback(knockbackDuration);
        }

        UpdateHealthBar();

        if (health <= 0)
        {
            GameManager manager =
                FindAnyObjectByType<GameManager>();

            if (manager != null)
            {
                manager.AddScore(scoreValue);
            }

            WaveManager waveManager =
                FindAnyObjectByType<WaveManager>();

            if (waveManager != null)
            {
                waveManager.ZombieKilled();
            }

            if (deathEffect != null)
            {
                Instantiate(
                    deathEffect,
                    transform.position,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
        }
    }

    void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float healthPercent =
                (float)health / maxHealth;

            healthBarFill.localScale =
                new Vector3(
                    1.5f * healthPercent,
                    0.15f,
                    1f
                );

            healthBarFill.localPosition =
                new Vector3(
                    -0.75f +
                    (0.75f * healthPercent),
                    0,
                    -0.01f
                );
        }
    }
}