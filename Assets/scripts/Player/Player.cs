using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Regeneration")]
    private float timeSinceLastHealthDamage = 0f; // Zeit seit dem letzten Schaden an der Gesundheit

    [Header("Text & Slider Fields")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] SliderBar healthBar;
    [SerializeField] SliderBar shieldBar;


    private Animator anim;
    private Rigidbody2D rb;

    private bool dead = false;
    private float moveHorizontal, moveVertical;
    private Vector2 movement;


    //public PlayerData playerData; // Stelle sicher, dass dies im Inspector zugewiesen ist
    [SerializeField] private PlayerData playerData; // Reference to PlayerData
    [SerializeField] private PlayerAimWeapon playerAimWeapon; // Reference to PlayerAimWeapon



    private void Start()
    {
        // Initialize the PlayerAimWeapon with PlayerData
        if (playerAimWeapon != null && playerData != null)
        {
            playerAimWeapon.Initialize(playerData);
        }

        else
        {
            Debug.LogError("PlayerAimWeapon or PlayerData is not assigned in the Inspector!");
        }
        // Initialisiere PlayerData mit Werten, die im Inspector bearbeitet wurden
        if (playerData == null)
        {
            playerData = new PlayerData(100, 5f, 1.0f, 0.1f, 10, 5.0f, 1, 1000, 0.2f, 2.0f, 50, 5f, 2.0f, 6.0f);
        }

        // Beispiel für den Zugriff auf die Werte
        Debug.Log("Max HP: " + playerData.maxHp);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initialisiere die Gesundheit und das Schild
        UpdateHealthUI();
        UpdateShieldUI();
    }

    private void Update()
    {

        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }
            // Regeneriere Gesundheit
            if (playerData.currentHp < playerData.maxHp)
            {
                playerData.RegenerateHealth(Time.deltaTime);
                UpdateHealthUI();
            }

            // Schildregeneration
            if (playerData.currentShield < playerData.maxShield)
            {
                playerData.RegenerateShield(Time.deltaTime);
                UpdateShieldUI();

            }



        // Bewegung
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical);
        anim.SetFloat("velocity", movement.magnitude);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * playerData.moveSpeed; // Verwende die Bewegungsgeschwindigkeit
    }

    public void TakeDamage(int damageAmount)
    {


        // Schaden anwenden
        if (playerData.currentShield > 0)
        {
            // Wenn das Schild noch vorhanden ist, ziehe den Schaden vom Schild ab
            int shieldDamage = Mathf.Min(damageAmount, playerData.currentShield);
            playerData.currentShield -= shieldDamage;
            damageAmount -= shieldDamage; // Reduziere den Schaden um den Betrag, der vom Schild absorbiert wurde
            anim.SetTrigger("hit");
            // Aktualisiere die Schildanzeige
            UpdateShieldUI();
        }

        // Wenn nach dem Schild noch Schaden übrig ist, ziehe ihn von der Gesundheit ab
        if (damageAmount > 0)
        {
            playerData.currentHp -= damageAmount;
            anim.SetTrigger("hit");
            UpdateHealthUI();

            if (playerData.currentHp <= 0)
            {
                Die();
            }

        }

        // Setze die Zeit seit dem letzten Schaden zurück
        timeSinceLastHealthDamage = 0f;
    }

    private void UpdateHealthUI()
    {
        healthText.text = playerData.currentHp.ToString();
        healthBar.SetCurrentValue(playerData.currentHp);
    }

    private void UpdateShieldUI()
    {
        shieldText.text = playerData.currentShield.ToString();
        shieldBar.SetCurrentValue(playerData.currentShield);
    }

    private void Die()
    {
        dead = true;
        GameManager.instance.GameOver();
    }
}