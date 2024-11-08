using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public Image shieldImage; // Das UI-Element für das Schild
    public Sprite fullSprite; // Sprite für 100%
    public Sprite mediumSprite; // Sprite für 60%
    public Sprite lowSprite; // Sprite für 30%

    private float shieldHealth = 100f; // Aktuelle Gesundheit des Schildes

    void Update()
    {
        // Hier kannst du die Gesundheit des Schildes ändern, um das Sprite zu testen
        // Zum Beispiel: shieldHealth -= Time.deltaTime * 10; // Verringert die Gesundheit

        UpdateShieldSprite();
    }

    void UpdateShieldSprite()
    {
        if (shieldHealth <= 0)
        {
            shieldImage.enabled = false; // Schild ausblenden
        }
        else if (shieldHealth <= 30)
        {
            shieldImage.sprite = lowSprite; // Sprite für 30%
        }
        else if (shieldHealth <= 60)
        {
            shieldImage.sprite = mediumSprite; // Sprite für 60%
        }
        else
        {
            shieldImage.sprite = fullSprite; // Sprite für 100%
        }
    }
}