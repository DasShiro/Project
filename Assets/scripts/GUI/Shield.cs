using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public Image shieldImage; // Das UI-Element f�r das Schild
    public Sprite fullSprite; // Sprite f�r 100%
    public Sprite mediumSprite; // Sprite f�r 60%
    public Sprite lowSprite; // Sprite f�r 30%

    private float shieldHealth = 100f; // Aktuelle Gesundheit des Schildes

    void Update()
    {
        // Hier kannst du die Gesundheit des Schildes �ndern, um das Sprite zu testen
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
            shieldImage.sprite = lowSprite; // Sprite f�r 30%
        }
        else if (shieldHealth <= 60)
        {
            shieldImage.sprite = mediumSprite; // Sprite f�r 60%
        }
        else
        {
            shieldImage.sprite = fullSprite; // Sprite f�r 100%
        }
    }
}