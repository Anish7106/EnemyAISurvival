using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y).normalized;

        transform.Translate(movement * speed * Time.deltaTime);

        Vector3 position = transform.position;

        Vector3 min = Camera.main.ViewportToWorldPoint(
            new Vector3(0.05f, 0.05f, 10f)
        );

        Vector3 max = Camera.main.ViewportToWorldPoint(
            new Vector3(0.95f, 0.95f, 10f)
        );

        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        transform.position = position;
    }
}