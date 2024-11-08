using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Das UI-Element f�r das Pausenmen�
    private bool isPaused = false; // Status, ob das Spiel pausiert ist

    void Update()
    {
        // �berpr�fen, ob die Escape-Taste gedr�ckt wurde
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
        pauseMenuUI.SetActive(false); // Pausenmen� ausblenden
        Time.timeScale = 1f; // Spielzeit fortsetzen
        isPaused = false; // Status aktualisieren
        // Erlaube die Eingaben wieder
        EnableGameplayInputs(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Pausenmen� anzeigen
        Time.timeScale = 0f; // Spielzeit anhalten
        isPaused = true; // Status aktualisieren
        // Verhindere Eingaben w�hrend des Pausenmen�s
        EnableGameplayInputs(false);
    }

    public void QuitGame()
    {
        Application.Quit(); // Spiel beenden
        Debug.Log("Quit Game"); // F�r Debugging-Zwecke
    }

    private void EnableGameplayInputs(bool enable)
    {
        // Hier kannst du die Eingaben f�r das Gameplay aktivieren oder deaktivieren
        // Beispiel: Wenn du einen Spielersteuerungs-Skript hast, kannst du dessen Eingaben hier steuern
        // playerController.enabled = enable; // Beispiel f�r einen PlayerController
    }
}