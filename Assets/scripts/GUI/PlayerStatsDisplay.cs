using UnityEngine;
using TMPro; // Verwende TextMesh Pro

public class PlayerStatsDisplay : MonoBehaviour
{
    public PlayerData playerData; // Referenz zur Player-Klasse
    public ExperienceManager experienceManager;
    public GameObject statsPanel; // Das Panel, das die Stats anzeigt
    public TextMeshProUGUI statsText; // Textfeld für die Stats (TMPro)

    private bool isStatsVisible = false; // Status, ob die Stats sichtbar sind

    void Start()
    {
        // Stelle sicher, dass das Stats-Panel zu Beginn nicht sichtbar ist
        statsPanel.SetActive(false);
    }

    void Update()
    {
        // Überprüfe, ob die Tabulator-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStats();
        }
    }

    void ToggleStats()
    {
        // Wechsle den Sichtbarkeitsstatus des Stats-Panels
        isStatsVisible = !isStatsVisible;
        statsPanel.SetActive(isStatsVisible);

        // Aktualisiere die Stats-Anzeige
        if (isStatsVisible)
        {
            UpdateStatsDisplay();
        }
    }

    public void UpdateStatsDisplay()
    {
        // Setze den Text des Stats-Feldes basierend auf den Werten aus der Player-Klasse
        statsText.text = $"Gesundheit: {playerData.currentHp}/{playerData.maxHp}\n" +
                         $"Schild: {playerData.currentShield}/{playerData.maxShield}\n"; // Erfahrung hinzufügen
    }
}