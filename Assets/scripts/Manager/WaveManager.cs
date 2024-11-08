using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public Slider countdownSlider;  // Referenz auf den Slider
    public TextMeshProUGUI countdownText;      // Referenz auf den Text
    public TextMeshProUGUI waveText; // Welle anzeigen
    public TextMeshProUGUI nextWaveText; // Text f�r die n�chste Welle
    public float countdownTime = 30f; // Zeit in Sekunden f�r die Countdown-Welle
    public float nextWaveTime = 10f; // Zeit in Sekunden f�r die n�chste Welle (anpassbar)

    private Image fillImage; // Referenz auf das F�llbild des Sliders
    private int currentWave = 0; // Aktuelle Welle
    private bool waveRunning = false; // Ist die Welle aktiv?
    private bool canStartNextWave = false; // Flag, um zu �berpr�fen, ob die n�chste Welle gestartet werden kann

    public static WaveManager instance;

    private void Start()
    {
        countdownSlider.maxValue = countdownTime;
        countdownSlider.value = countdownTime;

        // Hole das F�llbild des Sliders
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
            Destroy(gameObject); // Zerst�rt das zus�tzliche GameObject, wenn eine Instanz bereits existiert
        }
    }

    private void StartNewWave()
    {
        if (canStartNextWave) // �berpr�fen, ob die n�chste Welle gestartet werden kann
        {
            currentWave++;
            waveRunning = true;
            canStartNextWave = false; // Flag zur�cksetzen
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
            countdownText.text = Mathf.Ceil(timeRemaining).ToString(); // Runde auf die n�chste ganze Zahl

            // Wenn nur noch 10 Sekunden Zeit sind, �ndere die F�llfarbe des Sliders zu Rot
            if (timeRemaining <= 10f)
            {
                fillImage.color = Color.red;
            }
            else
            {
                fillImage.color = Color.green;
            }

            yield return null; // Warten auf den n�chsten Frame
        }

        EndWave();
    }

    private void EndWave()
    {
        waveRunning = false; // Welle als nicht aktiv markieren
        countdownSlider.value = 0;
        countdownText.text = "0";

        // Hier kannst du die Logik f�r das Ende der Welle hinzuf�gen
        waveText.color = Color.green;
        waveText.text = "Wave " + currentWave + " completed!";

        // Zerst�re alle aktiven Gegner
        EnemyManager.Instance.DestroyAllEnemies();

        // Setze das Flag, um zu �berpr�fen, ob die n�chste Welle gestartet werden kann
        canStartNextWave = true;

        // Starte den Countdown f�r die n�chste Welle
        StartCoroutine(StartNextWaveAfterDelay(nextWaveTime)); // Verwende die nextWaveTime
    }

    private IEnumerator StartNextWaveAfterDelay(float delay)
    {
        float timeRemaining = delay;

        while (timeRemaining > 0)
        {
            nextWaveText.text = "Next Wave starts in: " + Mathf.Ceil(timeRemaining).ToString() + " seconds"; // Update den Text f�r die n�chste Welle
            timeRemaining -= Time.deltaTime; // Zeit abziehen
            yield return null; // Warten auf den n�chsten Frame
        }

        nextWaveText.text = ""; // Text zur�cksetzen
        StartNewWave(); // Starte die neue Welle, wenn die Zeit abgelaufen ist
    }

    public bool IsWaveRunning() => waveRunning; // Gibt den Status der Welle zur�ck
}