using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownSlider : MonoBehaviour
{
    public Slider countdownSlider;  // Referenz auf den Slider
    public TextMeshProUGUI countdownText;      // Referenz auf den Text
    public TextMeshProUGUI nextWaveText;
    public TextMeshProUGUI waveText;
    public float countdownTime = 30f; // Zeit in Sekunden

    private Image fillImage; // Referenz auf das F�llbild des Sliders
    private int currentWave = 0; // Aktuelle Welle
    private bool waveRunning = false; // Ist die Welle aktiv?
    public float nextWaveTime = 10f;



    private void Start()
    {
        countdownSlider.maxValue = countdownTime;
        countdownSlider.value = countdownTime;

        // Hole das F�llbild des Sliders
        fillImage = countdownSlider.fillRect.GetComponent<Image>();

        StartNewWave();
    }

    private void StartNewWave()
    {
        currentWave++;
        waveRunning = true;
        waveText.color = Color.white;
        waveText.text = "Wave: " + currentWave;
        StartCoroutine(Countdown());
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
        waveRunning = false;
        countdownSlider.value = 0;
        countdownText.text = "0";

        // Hier kannst du die Logik f�r das Ende der Welle hinzuf�gen
        waveText.color = Color.green;
        waveText.text = "Wave " + currentWave + " completed!";

        // Optional: Starten Sie die n�chste Welle nach einer kurzen Verz�gerung
        StartCoroutine(StartNextWaveAfterDelay(nextWaveTime)); // 3 Sekunden warten
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