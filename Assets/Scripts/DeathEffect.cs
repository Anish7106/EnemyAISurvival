using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float duration = 0.25f;

    private float timer;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float progress = timer / duration;

        transform.localScale = Vector3.Lerp(
            startScale,
            startScale * 2f,
            progress
        );

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = 1f - progress;
            sprite.color = color;
        }

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}