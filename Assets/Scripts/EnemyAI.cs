using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public float speed = 2f;

    public int damage = 10;
    public float attackCooldown = 1f;

    public float obstacleCheckDistance = 1.2f;
    public LayerMask obstacleLayer;

    private float nextAttackTime;
    private float knockbackTimer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;

            KeepInsideArena();

            return;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

        if (distance > detectionRange)
        {
            Idle();
        }
        else if (distance > attackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }

        KeepInsideArena();
    }

    public void Knockback(float duration)
    {
        knockbackTimer = duration;
    }

    void Idle()
    {
    }

    void Chase()
    {
        Vector2 direction =
            (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            obstacleCheckDistance,
            obstacleLayer
        );

        if (hit.collider != null)
        {
            Vector2 leftDirection =
                Vector2.Perpendicular(direction);

            Vector2 rightDirection =
                -Vector2.Perpendicular(direction);

            RaycastHit2D leftHit = Physics2D.Raycast(
                transform.position,
                leftDirection,
                obstacleCheckDistance,
                obstacleLayer
            );

            RaycastHit2D rightHit = Physics2D.Raycast(
                transform.position,
                rightDirection,
                obstacleCheckDistance,
                obstacleLayer
            );

            if (leftHit.collider == null)
            {
                direction = leftDirection;
            }
            else if (rightHit.collider == null)
            {
                direction = rightDirection;
            }
        }

        transform.position +=
            (Vector3)(
                direction *
                speed *
                Time.deltaTime
            );
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            PlayerHealth playerHealth =
                player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            nextAttackTime =
                Time.time + attackCooldown;
        }
    }

    void KeepInsideArena()
    {
        Vector3 position = transform.position;

        Vector3 min =
            Camera.main.ViewportToWorldPoint(
                new Vector3(0.05f, 0.05f, 10f)
            );

        Vector3 max =
            Camera.main.ViewportToWorldPoint(
                new Vector3(0.95f, 0.95f, 10f)
            );

        position.x =
            Mathf.Clamp(
                position.x,
                min.x,
                max.x
            );

        position.y =
            Mathf.Clamp(
                position.y,
                min.y,
                max.y
            );

        transform.position = position;
    }
}