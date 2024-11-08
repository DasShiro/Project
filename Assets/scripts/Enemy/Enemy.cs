using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currentHealth; // Current health of the enemy
    private ScoreManager scoreManager;
    private Transform playerTransform; // Reference to the player's transform
    public Transform player;
    private int expAmount;

    private PlayerData playerData;
    public EnemyData enemyData; // Reference to the ScriptableObject

    private Animator anim;
    public EnemyManager enemyManager; // Reference to the EnemyManager

    public ExperienceManager experienceManager;  // Reference to ExperienceManager
    private Rigidbody2D rb;

    [SerializeField] private GameObject floatingTextPrefab;

    private void Start()
    {
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        enemyManager = Object.FindFirstObjectByType<EnemyManager>();
        experienceManager = Object.FindFirstObjectByType<ExperienceManager>();
        anim = GetComponent<Animator>(); // Get Animator component
        rb = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindWithTag("Player").transform; // Ensure the player has the "Player" tag
        if (playerTransform == null)
        {
            Debug.LogError("Player transform is null! Make sure the player GameObject is tagged correctly.");
        }
        Initialize();
        Initialize(scoreManager, enemyManager, playerTransform, experienceManager); // Pass the references
    }

    private void Initialize()
    {
        if (enemyData == null)
        {
            return;
        }

        currentHealth = enemyData.health; // Initialize current health
        expAmount = enemyData.expAmount;
    }

    public void Initialize(ScoreManager scoreManager, EnemyManager enemyManager, Transform player, ExperienceManager experienceManager)
    {
        this.scoreManager = scoreManager; // Set the reference to ScoreManager
        this.enemyManager = enemyManager; // Set the reference to EnemyManager
        this.experienceManager = experienceManager; // Corrected this line
        this.playerTransform = player; // Set the player reference


    }

    public void SetPlayerReference(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void FixedUpdate()
    {

        if (!WaveManager.instance.IsWaveRunning())
        {
            return;
        }

        if (playerTransform != null)
        {
            MoveTowardsPlayer();
        }


    }

    private void MoveTowardsPlayer()
    {
        // Berechne die Richtung zum Spieler
        Vector2 direction = (playerTransform.position - transform.position).normalized; // Normalisierte Richtung
        float moveSpeed = enemyData.speed; // Geschwindigkeit aus den Daten des Gegners

        // Bewege den Gegner in die Richtung des Spielers
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); // Bewegung
    }

    private void AttackPlayer()
    {
        Player player = playerTransform.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(enemyData.damage);
        }
    }


    public void TakeDamage(int damageAmount, DamageType damageType)
    {
        if (damageAmount <= 0)
        {
            Debug.LogWarning("Damage amount is zero or negative!");
            return; // Verhindere, dass der Schaden angewendet wird
        }

        currentHealth -= damageAmount; // Verwende den Schaden direkt

        // Überprüfe, ob die Gesundheit <= 0 ist
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Trigger die Trefferanimation
            anim.SetTrigger("hit");
        }
    }

    public void Die()
    {
        if (scoreManager != null)
        {
            scoreManager.AddPoints(enemyData.pointsOnDeath);
        }

        if (experienceManager != null)
        {
            ExperienceManager.Instance.AddExperience(enemyData.expAmount);
        }


        DropItems();

        if (enemyManager != null)
        {
            enemyManager.RemoveEnemy(this);
        }

        Destroy(gameObject);
    }

    private void DropItems()
    {
        foreach (var item in enemyData.dropItems)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Überprüfe, ob der Spieler mit dem Gegner kollidiert
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(enemyData.damage); // Schaden am Spieler
            Debug.Log("Player was hit by enemy!");
        }
    }


    public void Hit(int damageAmount)
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData is not initialized in Enemy script.");
            return; // Prevent further execution if playerData is null
        }

        // Assuming ranged damage is taken from playerData
        TakeDamage((int)playerData.rangedDamage, DamageType.Ranged);
    }

    //void ShowDamage(string text)
    //{
    //    if(floatingTextPrefab)
    //    {
    //        GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
    //        //prefab.GetComponentsInChildren<TextMesh>().text = text;
    //    }
    //}

}