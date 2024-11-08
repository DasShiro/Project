using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 18f; // Geschwindigkeit des Projektils
    public PlayerData playerData; // Referenz auf die PlayerData-Instanz

    private void Start()
    {
        // Hier musst du sicherstellen, dass du die PlayerData vom Spieler erhältst
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerData = playerData; // Zugriff auf die PlayerData des Spielers
            if (playerData == null)
            {
                Debug.LogError("PlayerData is null!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    // Diese Methode wird aufgerufen, um die PlayerData des Spielers zu setzen
    public void Initialize(PlayerData data)
    {
        playerData = data; // Setze die PlayerData-Referenz
    }

    private void FixedUpdate()
    {
        // Bewege das Projektil in eine Richtung
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (playerData != null) // Check if playerData is not null
            {
                int damageAmount = playerData.rangedDamage; // Get the ranged damage from playerData
                Debug.Log("Damage Amount: " + damageAmount); // Log the damage amount
                enemy.TakeDamage(damageAmount, DamageType.Ranged); // Pass the damage type as well
            }
            else
            {
                Debug.LogError("PlayerData is null when trying to deal damage!");
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Did not hit enemy. Hit object: " + collision.gameObject.name);
        }
    }

}

