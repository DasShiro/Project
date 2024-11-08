using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Das UI-Element für das Pausenmenü
    private bool isPaused = false; // Status, ob das Spiel pausiert ist

    void Update()
    {
        // Überprüfen, ob die Escape-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume(); // Spiel fortsetzen
            }
            else
            {
                Pause(); // Spiel pausieren
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Pausenmenü ausblenden
        Time.timeScale = 1f; // Spielzeit fortsetzen
        isPaused = false; // Status aktualisieren
        // Erlaube die Eingaben wieder
        EnableGameplayInputs(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Pausenmenü anzeigen
        Time.timeScale = 0f; // Spielzeit anhalten
        isPaused = true; // Status aktualisieren
        // Verhindere Eingaben während des Pausenmenüs
        EnableGameplayInputs(false);
    }

    public void QuitGame()
    {
        Application.Quit(); // Spiel beenden
        Debug.Log("Quit Game"); // Für Debugging-Zwecke
    }

    private void EnableGameplayInputs(bool enable)
    {
        // Hier kannst du die Eingaben für das Gameplay aktivieren oder deaktivieren
        // Beispiel: Wenn du einen Spielersteuerungs-Skript hast, kannst du dessen Eingaben hier steuern
        // playerController.enabled = enable; // Beispiel für einen PlayerController
    }
}