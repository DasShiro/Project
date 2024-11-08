using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab; // Das Prefab für die Waffe
    [SerializeField] GameObject swordPrefab; // Das Prefab für das Schwert

    Transform player;
    List<Vector2> gunPosition = new List<Vector2>();
    List<Vector2> swordPosition = new List<Vector2>();

    int spawnedGuns = 0;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        //// Definiere die Positionen, an denen die Waffen gespawnt werden
        //gunPosition.Add(new Vector2(-1.2f, 1f));
        //gunPosition.Add(new Vector2(1.2f, 1f));
        //gunPosition.Add(new Vector2(-1.4f, 0.2f));
        //gunPosition.Add(new Vector2(1.4f, 0.2f));
        //gunPosition.Add(new Vector2(-1f, -0.5f));
        //gunPosition.Add(new Vector2(1f, -0.5f));


        // Füge zwei Waffen hinzu
        //AddGun();
        //AddGun();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    AddGun();

        // Rotiere das Schwert um den Spieler
       // RotateSwordAroundPlayer();
    }

    //void AddGun()
    //{
    //    if (spawnedGuns < gunPosition.Count)
    //    {
    //        var pos = gunPosition[spawnedGuns];
    //        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);
    //        newGun.GetComponent<Gun>().SetOffset(pos);
    //        spawnedGuns++;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Alle Waffenslots sind bereits belegt!");
    //    }
    }

    //void AddSword()
    //{
    //    // Füge das Schwert an der definierten Position hinzu
    //    var swordPos = swordPosition[0]; // Hier wird die erste Position verwendet
    //    var newSword = Instantiate(swordPrefab, swordPos, Quaternion.identity);
    //    newSword.tag = "Sword"; // Setze den Tag des Schwerts
    //}

    //void RotateSwordAroundPlayer()
    //{
    //    // Suche nach dem Schwert-Objekt
    //    GameObject sword = GameObject.FindWithTag("Sword"); // Stelle sicher, dass dein Schwert-Objekt den Tag "Sword" hat

    //    if (sword != null)
    //    {
    //        // Bestimme den Rotationswinkel (z.B. 50 Grad pro Sekunde)
    //        float rotationSpeed = 50f * Time.deltaTime; // Geschwindigkeit der Rotation
    //        sword.transform.RotateAround(player.position, Vector3.forward, rotationSpeed); // Rotiert um die Z-Achse des Spielers
    //    }
    //}
