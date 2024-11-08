using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private float meeleeSpeed;
    //[SerializeField] private int damage;

    private PlayerData playerData;
    private float timeUntilMeele;

    public void Initialize(PlayerData data)
    {
        playerData = data; // Setze die PlayerData-Referenz
    }

    private void Update()
    {
        if (timeUntilMeele <= 0f)
        {
            if (Input.GetMouseButtonDown(1)) // Rechtsklick für den Schwertangriff
            {
                anim.SetTrigger("sword_attack");
                timeUntilMeele = meeleeSpeed;
            }
        }
        else
        {
            timeUntilMeele -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name); // Dies zeigt dir, welches Objekt kollidiert

        // Überprüfe, ob der kollidierte Gegner ein Enemy ist
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Wenn es ein Gegner ist, wird der Schaden an ihm angewendet
            Debug.Log("Enemy hit by sword");
            // Hier kannst du den Schaden am Gegner anwenden
            enemy.TakeDamage(playerData.physicalDamage, DamageType.Physical); // Schaden anwenden
        }
        else
        {
            // Wenn der Spieler mit dem Schwert kollidiert, passiert nichts
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Player collided with sword!");
                // Hier sollte der Spieler keinen Schaden erleiden
            }
        }
    }
}


