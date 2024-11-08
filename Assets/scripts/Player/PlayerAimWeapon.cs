using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform muzzlePosition; // Dies sollte der Ort sein, an dem das Projektil erzeugt wird.
    private Transform aimTransform; // Der Transform der Waffe, der sich bewegen soll.

    [Header("Config")]
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float projectileSpeed = 10f;

    private float timeSinceLastShot = 0f;

    private PlayerData playerData;


    public void Initialize(PlayerData data)
    {
        playerData = data; // Setze die PlayerData-Referenz
    }

    private void Awake()
    {
        aimTransform = transform.Find("Weapon"); // Sicherstellen, dass "Aim" der Name des Waffentransforms ist.
    }

    private void Update()
    {
        AimAtMouse();
        Shooting();
    }

    void AimAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Setze z auf 0 f�r 2D

        Vector3 direction = mousePosition - aimTransform.position; // Verwende die Position der Waffe
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0, 0, angle); // Rotiert nur die Waffe
    }

    void Shooting()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0) && timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzlePosition.position, muzzlePosition.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Setze z auf 0 für 2D
            Vector2 direction = (mousePosition - muzzlePosition.position).normalized;
            rb.linearVelocity = direction * projectileSpeed; // Setze die Geschwindigkeit des Projektils
        }

        // Hole die Projectile-Komponente und initialisiere sie mit der PlayerData
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(playerData); // Übergebe die PlayerData an das Projektil
        }

        Destroy(projectile, 3); // Zerstöre nach 3 Sekunden
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob das Projektil einen Feind getroffen hat
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (playerData != null)
            {
                // Verwende den rangedDamage des Spielers
                enemy.TakeDamage(playerData.rangedDamage, DamageType.Ranged); // Assuming you have this method in your Enemy class
                Destroy(gameObject); // Zerstöre das Projektil nach dem Treffer
            }
            else
            {
                Debug.LogError("PlayerData is null when trying to deal damage!");
            }
        }
        else
        {
            Debug.LogWarning("No Enemy component found on the object.");
        }
    }
}