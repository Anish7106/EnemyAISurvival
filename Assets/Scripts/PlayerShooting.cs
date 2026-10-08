using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePosition =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction =
            mousePosition - firePoint.position;

        direction.Normalize();

        GameObject newBullet = Instantiate(
            bullet,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb =
            newBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        ShowMuzzleFlash();
    }

    void ShowMuzzleFlash()
    {
        muzzleFlash.SetActive(true);

        Invoke("HideMuzzleFlash", 0.05f);
    }

    void HideMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}