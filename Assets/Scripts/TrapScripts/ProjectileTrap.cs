// Brennan Wathke 5/17/2025
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float projectileLifetime = 3f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float damageAmount = 20f;

    private float shootTimer = 0f;
    private bool canShoot = true;

    private void Update()
    {
        if (canShoot)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();

        if (projectileController == null)
        {
            projectileController = projectile.AddComponent<ProjectileController>();
        }

        Vector2 shootDirection = (shootPoint.position - transform.position).normalized;

        projectileController.Initialize(projectileSpeed, projectileLifetime, damageAmount, shootDirection);
    }

    public void SetCanShoot(bool shooting)
    {
        canShoot = shooting;
    }
}

public class ProjectileController : MonoBehaviour
{
    private float speed;
    private float lifetime;
    private float damageAmount;
    private Rigidbody2D rb;

    public void Initialize(float projectileSpeed, float projectileLifetime, float damage, Vector2 direction)
    {
        speed = projectileSpeed;
        lifetime = projectileLifetime;
        damageAmount = damage;

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        rb.linearVelocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInformation playerInfo = collision.GetComponent<PlayerInformation>();

            if (playerInfo != null)
            {
                playerInfo.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            Destroy(gameObject);
        }
    }
}