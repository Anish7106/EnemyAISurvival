using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(transform.position);
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}