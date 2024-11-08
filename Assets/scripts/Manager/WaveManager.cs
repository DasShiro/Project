using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public Slider countdownSlider;  // Referenz auf den Slider
    public TextMeshProUGUI countdownText;      // Referenz auf den Text
    public TextMeshProUGUI waveText; // Welle anzeigen
    public TextMeshProUGUI nextWaveText; // Text für die nächste Welle
    public float countdownTime = 30f; // Zeit in Sekunden für die Countdown-Welle
    public float nextWaveTime = 10f; // Zeit in Sekunden für die nächste Welle (anpassbar)

    private Image fillImage; // Referenz auf das Füllbild des Sliders
    private int currentWave = 0; // Aktuelle Welle
    private bool waveRunning = false; // Ist die Welle aktiv?
    private bool canStartNextWave = false; // Flag, um zu überprüfen, ob die nächste Welle gestartet werden kann

    public static WaveManager instance;

    private void Start()
    {
        countdownSlider.maxValue = countdownTime;
        countdownSlider.value = countdownTime;

        // Hole das Füllbild des Sliders
        fillImage = countdownSlider.fillRect.GetComponent<Image>();
        canStartNextWave = true; // Allow starting the first wave
        StartNewWave(); // Start the first wave
    }

    private void Awake()
    {
        // Singleton-Implementierung
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Zerstört das zusätzliche GameObject, wenn eine Instanz bereits existiert
        }
    }

    private void StartNewWave()
    {
        if (canStartNextWave) // Überprüfen, ob die nächste Welle gestartet werden kann
        {
            currentWave++;
            waveRunning = true;
            canStartNextWave = false; // Flag zurücksetzen
            waveText.text = "Wave: " + currentWave;

            Debug.Log("Starting Wave: " + currentWave); // Debug-Log

            // Rufe die Logik zum Spawnen von Gegnern hier auf
            EnemyManager.Instance.StartSpawningEnemies(currentWave); // Stelle sicher, dass diese Methode existiert

            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            countdownSlider.value = timeRemaining;
            countdownText.text = Mathf.Ceil(timeRemaining).ToString(); // Runde auf die nächste ganze Zahl

            // Wenn nur noch 10 Sekunden Zeit sind, ändere die Füllfarbe des Sliders zu Rot
            if (timeRemaining <= 10f)
            {
                fillImage.color = Color.red;
            }
            else
            {
                fillImage.color = Color.green;
            }

            yield return null; // Warten auf den nächsten Frame
        }

        EndWave();
    }

    private void EndWave()
    {
        waveRunning = false; // Welle als nicht aktiv markieren
        countdownSlider.value = 0;
        countdownText.text = "0";

        // Hier kannst du die Logik für das Ende der Welle hinzufügen
        waveText.color = Color.green;
        waveText.text = "Wave " + currentWave + " completed!";

        // Zerstöre alle aktiven Gegner
        EnemyManager.Instance.DestroyAllEnemies();

        // Setze das Flag, um zu überprüfen, ob die nächste Welle gestartet werden kann
        canStartNextWave = true;

        // Starte den Countdown für die nächste Welle
        StartCoroutine(StartNextWaveAfterDelay(nextWaveTime)); // Verwende die nextWaveTime
    }

    private IEnumerator StartNextWaveAfterDelay(float delay)
    {
        float timeRemaining = delay;

        while (timeRemaining > 0)
        {
            nextWaveText.text = "Next Wave starts in: " + Mathf.Ceil(timeRemaining).ToString() + " seconds"; // Update den Text für die nächste Welle
            timeRemaining -= Time.deltaTime; // Zeit abziehen
            yield return null; // Warten auf den nächsten Frame
        }

        nextWaveText.text = ""; // Text zurücksetzen
        StartNewWave(); // Starte die neue Welle, wenn die Zeit abgelaufen ist
    }

    public bool IsWaveRunning() => waveRunning; // Gibt den Status der Welle zurück
}